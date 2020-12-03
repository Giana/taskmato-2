var currTimer;

function updateTimerDisplay(minutes, seconds) {
    document.getElementById('timer').innerHTML = minutes + ":" + seconds;
};

function runTimer(duration) {
    var timerVar = duration, minutes, seconds;

    if (!currTimer) {
        currTimer = setInterval(function () {
            minutes = parseInt(timerVar / 60, 10);
            seconds = parseInt(timerVar % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            updateTimerDisplay(minutes, seconds);

            if (--timerVar < 0) {
                resetTimer();
            }
        }, 1000);
    }
};

function resetTimer() {
    clearInterval(currTimer);
    currTimer = null;
    document.getElementById('timer').innerHTML = "00:00";
};

window.onload = function () {
    resetTimer();
};