$(function () {
    var youChoice       = $('.you-choice');
    var compChoice      = $('.comp-choice');
    var highlight       = $('.highlight');
    var spins           = $('.spins');
    var stat            = $('.stat');
    var choiceButtons   = $('.choice-btn');
    var reset           = $('.reset');
    var spin            = $('.spin');
    var dismiss         = $('.dismiss');
    var splash          = $('.splash-screen');
    var splash_msg      = $('.msg');
    var splash_msg_std  = 'Välj ett nummer innan ni använder knappen "snurra"!';
    var splash_tinker   = 'Stop tinkering with the values!';
    var choice_min      = 1;
    var choice_max      = 6;

    function showSplash(msg) {
        (msg == null || msg.length == 0) ? splash_msg.text(splash_msg_std) : splash_msg.text(msg);
        splash.show();
    }

    function hideSplash() { splash.hide(); }

    function historyCount(arr) {
        var count = 0;

        $.each(arr, function (index, value) {
            if (value == 1) { count++; }
        });
        return count;
    }

    function formatStat(part) {
        return (Math.round(part * 100));
    }

    dismiss.on('click', function () {
        hideSplash();
    });

    choiceButtons.on('click', function () {
        var pressedButton = $(this);
        var choice = Number(pressedButton.text());
        pressedButton.addClass('flashbutton');

        if (choice >= choice_min && choice <= choice_max) {
            youChoice.text(choice).addClass('flashtext'); // Add some css change so user notices the selection
            compChoice.text('-');
        } else {
            // Show splash-screen error since someone is tinkerning with the html DOM values
            showSplash(splash_tinker);
        }
    });

    spin.on('click', function () {
        var url = '/Home/Spin/';
        var choice = Number(youChoice.text());  // Picking from the selected choice DOM element

        if (choice >= choice_min && choice <= choice_max) {
            var payload = { choice: choice }

            // Send ajax request to server (Note: only a successhandler, always expect a result from srv)
            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(payload),
                success: function (response) {
                    if (response.status !== 'error') {
                        // Update gameboard
                        youChoice.removeClass('flashtext');
                        choiceButtons.removeClass('flashbutton');
                        spins.text(response.spins);
                        stat.text(formatStat(historyCount(response.history) / Number(response.spins)));
                        compChoice.text(response.compChoice);
                        (choice == response.compChoice) ? highlight.text('Du klarade dig') : highlight.text('Bom');
                    } else {
                        showSplash('Ett fel uppstod och applicationen kunde inte ta emot er förfrågan!');
                    }
                }
            });
        } else {
            // Show splash-screen standard msg that a number must be selected
            showSplash('');
        }
    });

    reset.on('click', function () {
        var url = '/Home/Reset/';

        // Send ajax request to server
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: '',
            success: function (response) {
                if (response.status !== 'error') {

                    // Clearing view if srv completes
                    if (true) {
                        // Reset UI DOM values
                        youChoice.text('-');
                        compChoice.text('-');
                        stat.text('0');
                        spins.text('0');
                        highlight.text('---');
                        showSplash('Ert tidigare resultat är nu rensat!');
                    } else {
                        showSplash('Ett fel uppstod och application kunde inte rensa er tidigare statistik!');
                    }

                } else {
                    console.log('Error: ' + response);
                }
            }
        });
    });
});