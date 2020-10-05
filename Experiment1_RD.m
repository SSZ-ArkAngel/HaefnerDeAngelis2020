

% % Clear the workspace and the screen
% sca;
% close all;
% clearvars;


%--------------------------------------------
%               DELETE LATER
%--------------------------------------------

%AssertOpenGL;
%Screen('Preference', 'SkipSyncTests', 0 );

% while the code above works for single displays, I have tweaked it to
% account for multiple ones

%Screen ('Preference', 'SkipSyncTests', max(screens));



%------------------------------------------------
%                   Set up
%------------------------------------------------ 

KbName('UnifyKeyNames')

% Get the screen number (0)
screens = Screen('Screens');

% Draw to the external screen if avaliable
screenNumber = max(screens);

AssertOpenGL;
%Screen('Preference', 'SkipSyncTests', 0 );

% while the code above works for single displays, I have tweaked it to
% account for multiple ones

Screen ('Preference', 'SkipSyncTests', max(screens));
    
% Define black and white
white = WhiteIndex(screenNumber);
black = BlackIndex(screenNumber);

[win, windowRect] = Screen('OpenWindow', screenNumber, black);

[window, windowRect] = Screen('OpenWindow', screenNumber, black);

% Alpha blending (smoothing) - don't worry about this
Screen('BlendFunction', window, GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
[center(1), center(2)] = RectCenter(windowRect);
fps=Screen('FrameRate',window);      % frames per second
ifi=Screen('GetFlipInterval', window);
if fps==0
   fps=1/ifi;
end;

% Hide the mouse cursor
HideCursor;
  
Priority(MaxPriority(window));

% Initial flip
vbl=Screen('Flip', window);


%------------------------------------------------
%                   Data Logging
%------------------------------------------------ 



%-------------------------------------------------
%               Optic flow
%------------------------------------------------

showSprites = 0;

% ------------------------
%  Optic field parameters
% ------------------------

nframes     = 3600; % [NOT SURE] number of animation frames in loop
window_width   = 39;   % [NOT SURE] horizontal dimension of viewable screen (cm)
v_dist      = 57.3;   % viewing distance (cm)
if showSprites > 0
    dot_speed   = 0.07; % dot speed (deg/sec) - Take it sloooow.
    f_kill      = 0.00; % Don't kill (m)any dots, so user can see better.
else
    dot_speed   = 7;    % dot speed (deg/sec)
    f_kill      = 0.05; % [pdf DID NOT SPECIFY] fraction of dots to kill each frame (limited lifetime)
end
ndots       = 500; % number of dots
annulus_r       = 19;   % radius of  annulus (degrees)
dot_w       = 0.1;  % width of dot (deg)
fix_r       = 0.15; % radius of fixation point (deg)

waitframes = 1;     % Show new dot-images at each waitframes'th monitor refresh.

% ---------------------------------------
% Initialize dot positions and velocities
% ---------------------------------------

ppd = pi * (windowRect(3)-windowRect(1)) / atan(window_width/v_dist/2) / 360;    % pixels per degree
dot_speed = dot_speed * ppd / fps; % pixels per frame
dot_size = dot_w * ppd;            % pixels
fix_cord = [center-fix_r*ppd center+fix_r*ppd];

r_pixel = annulus_r * ppd;	% radius of annulus (pixels from center)
r = r_pixel * sqrt(rand(ndots,1));	% r
t = 2*pi*rand(ndots,1);    % theta polar coordinate
cs = [cos(t), sin(t)];
xy = [r r] .* cs;   % dot positions in Cartesian coordinates (pixels from center)

dot_direction = abs(2 * floor(rand(ndots,1)+0.5) - 1);    % motion direction (in or out) for each dot
dr = dot_speed * dot_direction;                      % change in radius per frame (pixels)
dxdy = [dr dr] .* cs;                       % change in x and y per frame (pixels)

colvect=white;

% [NOT SURE] Clamp point sizes to range supported by graphics hardware:
[minsmooth,maxsmooth] = Screen('DrawDots', window)
dot_size = min(max(dot_size, minsmooth), maxsmooth);


%-------------------------------------------------
%               Target probe
%------------------------------------------------

rand('seed', sum(100 * clock));
[screenXpixels, screenYpixels] = Screen('WindowSize', window);
Screen('BlendFunction', window, GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
dotColor = [1 0 0];
dotXpos = rand * screenXpixels;
dotYpos = rand * screenYpixels;
probeTranslation = [dotXpos dotYpos];
dotSizePix = 20;
%Screen('DrawDots', window, [dotXpos dotYpos], dotSizePix, dotColor, [], 2);

%-------------------------------------------------
%           Screen Center
%-------------------------------------------------

screenCenterX = screenXpixels/2;
screenCenterY = screenYpixels/2;


%------------------------------------------------
%           Adjustable paddle
%------------------------------------------------

% A straight line at the center of the screen that can be manipulated by
% the arrow keys.

%space occupied by the line

Y1 = screenCenterY - 100;
Y2 = screenCenterY + 100;
X1 = screenCenterX;
X2 = screenCenterX;

cx = screenCenterX;
cy = screenCenterY;

%adjustmentSteps

degPerHit = 1;
deg = 90;

%Screen(‘DrawLine’, windowPtr [,color], fromH, fromV, toH, toV [,penWidth]);
%Screen('DrawLines', win, [x, x ; 0, h], lw, [0, 255; 0, 255; 0, 255])

%Screen('DrawLine', win, uint8(white), lineY1, lineX1, lineY2, lineX2, 2);
%Screen('DrawLine',window,[127],200,200, 400, 400);

escapeKey = KbName('ESCAPE');
enterKey = KbName('return');
leftKey = KbName('LeftArrow');
rightKey = KbName('RightArrow');

%----------------------------------------
%           Experimental loop
%----------------------------------------


% --------------
% Animation loop
% --------------
for i = 1:nframes
    if (i>1)
        %----------------
        % Fixation circle
        %----------------
        Screen('DrawDots', window, [dotXpos dotYpos], dotSizePix, colvect, [], 2);
        Screen('FillOval', window, uint8(white), fix_cord);  % Note: 'Flip' will erase this!
        Screen('DrawDots', window, xymatrix, dot_size, colvect, center, 1);
    end
    
    % Break out of animation loop key or mouse press
    [mx, my, buttons]=GetMouse(screenNumber);
    if any(buttons)
        break;
    end
    
    if KbCheck
        break;
    end
    
    xy = xy + dxdy; % move dots
    r = r + dr; % update polar coordinates
    dotYpos = dotYpos + 1;
    
    % Check if dots have gone beyond the borders of the annulus
    % maintains radial optic flow
    r_out = find(r > r_pixel | rand(ndots,1) < f_kill);
    nout = length(r_out);
    
    % If the dots have gone beyond the annulus...
    if nout
        % choose new coordinates
        r(r_out) = r_pixel * sqrt(rand(nout,1));
        t(r_out) = 2*pi*(rand(nout,1));
        
        % now convert the polar coordinates to Cartesian
        cs(r_out,:) = [cos(t(r_out)), sin(t(r_out))];
        xy(r_out,:) = [r(r_out) r(r_out)] .* cs(r_out,:);
        
        % compute the new cartesian velocities
        dxdy(r_out,:) = [dr(r_out) dr(r_out)] .* cs(r_out,:);
    end
    
    % [NOT SURE]
    xymatrix = transpose(xy);
    
    vbl=Screen('Flip', window, vbl + (waitframes-0.5)*ifi);
end

%wakeup=WaitSecs(3)
[win, windowRect] = Screen('OpenWindow', screenNumber, black);

% --------------
% Animation loop
% --------------
for i = 1:nframes
    if (i>1)
        %----------------
        % Draw Paddle
        %----------------
        
        Screen('DrawLine',win,[127],X1, Y1, X2, Y2, 5);
        %Screen('DrawLine', win, uint8(white), lineY1, lineX1, lineY2, lineX2, 2);
        
    end
    
    [keyCode] = KbCheck;
    
    %Translation!!!
    

%     right
%     deg = deg - 1;
%     X1 = cx + cosd(deg).*100;
%     Y1 = cy + sind(deg).*100;
%     X2 = cx - cosd(deg).*100;
%     Y2 = cy - sind(deg).*100;    

%     left
%     deg = deg + 1;
%     X1 = cx + cosd(deg).*100;
%     Y1 = cy + sind(deg).*100;
%     X2 = cx - cosd(deg).*100;
%     Y2 = cy - sind(deg).*100;
    

    respToBeMade = true;
        while respToBeMade
            [keyIsDown,secs, keyCode] = KbCheck;
            if keyCode(escapeKey)
                ShowCursor;
                sca;
                return
            elseif keyCode(leftKey)
                response = 1;
                %     left
                deg = deg - 1;
                X1 = cx + cosd(deg).*100;
                Y1 = cy + sind(deg).*100;
                X2 = cx - cosd(deg).*100;
                Y2 = cy - sind(deg).*100;
                respToBeMade = false;
            elseif keyCode(rightKey)
                %     right
                deg = deg + 1;
                X1 = cx + cosd(deg).*100;
                Y1 = cy + sind(deg).*100;
                X2 = cx - cosd(deg).*100;
                Y2 = cy - sind(deg).*100;
                respToBeMade = false;
            end
        end
    
   %animation break when enter is pressed
   
   
   
    vbl=Screen('Flip', win, vbl + (waitframes-0.5)*ifi);
end

Priority(0);
ShowCursor;
sca;
