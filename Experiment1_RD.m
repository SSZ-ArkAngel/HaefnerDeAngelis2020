% Clear the workspace and the screen
sca;
close all;
clearvars;


%


% Open an on screen window
% [window, windowRect] = PsychImaging('OpenWindow', screenNumber, white);

% Get the size of the on screen window
% [screenXpixels, screenYpixels] = Screen('WindowSize', window);

% Set the optic flow vector color
flowVecColor = [0 0 0];
drawCross();



%--------------------------------------------
%           Functions we need
%--------------------------------------------

% Drawing a fixation cross
function drawCross()

% Get the screen number (0)
screens = Screen('Screens');

% Draw to the external screen if avaliable
screenNumber = max(screens);

% Define black and white
white = WhiteIndex(screenNumber);
black = BlackIndex(screenNumber);

% Define cross characteristics
crossLength = 10;
crossColor = white;
crossWidth = 3;

% Set start and end points of lines
crossLines = [-crossLength, 0; crossLength, 0; 0, -crossLength; 0, crossLength];
crossLines = crossLines';

% Open the screen
[window, windowRect] = Screen('OpenWindow', screenNumber, black );

% Define the center of the screen
xCenter = windowRect(3)/2;
yCenter = windowRect (4)/2;

% Draw the lines
Screen('DrawLines', window, crossLines, crossWidth, crossColor, [xCenter, yCenter]);
Screen('Flip', window);

KbWait;
clear Screen;

end


%----------------------------------------
%           Experimental loop
%----------------------------------------








