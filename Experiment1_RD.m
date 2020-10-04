% % Clear the workspace and the screen
% sca;
% close all;
% clearvars;


%--------------------------------------------
%               DELETE LATER
%--------------------------------------------

AssertOpenGL;
Screen('Preference', 'SkipSyncTests', 0 );



%------------------------------------------------
%                   Set up
%------------------------------------------------ 


% Get the screen number (0)
screens = Screen('Screens');

% Draw to the external screen if avaliable
screenNumber = max(screens);
    
% Define black and white
white = WhiteIndex(screenNumber);
black = BlackIndex(screenNumber);
    
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
ndots       = 300; % number of dots
annulus_r       = 15;   % radius of  annulus (degrees)
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

dot_direction = 2 * floor(rand(ndots,1)+0.5) - 1;    % motion direction (in or out) for each dot
dr = dot_speed * dot_direction;                      % change in radius per frame (pixels)
dxdy = [dr dr] .* cs;                       % change in x and y per frame (pixels)

colvect=white;

% [NOT SURE] Clamp point sizes to range supported by graphics hardware:
[minsmooth,maxsmooth] = Screen('DrawDots', window)
dot_size = min(max(dot_size, minsmooth), maxsmooth);

% --------------
% Animation loop
% --------------
for i = 1:nframes
    if (i>1)
        %----------------
        % Fixation circle
        %----------------
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




%-------------------------------------------------
%               Target probe
%------------------------------------------------





%------------------------------------------------
%           Adjustable paddle
%------------------------------------------------






 

%----------------------------------------
%           Experimental loop
%----------------------------------------










Priority(0);
ShowCursor;
sca;

