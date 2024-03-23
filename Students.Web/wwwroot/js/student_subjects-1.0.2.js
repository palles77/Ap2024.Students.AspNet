function moveRight() {
    var availableSubjects = document.getElementById('availableSubjects');
    var chosenSubjects = document.getElementById('chosenSubjects');

    for (var i = 0; i < availableSubjects.options.length; i++) {
        var option = availableSubjects.options[i];

        if (option.selected) {
            chosenSubjects.options.add(new Option(option.text, option.value));
            availableSubjects.remove(i);
            i--;
        }
    }
}

function moveLeft() {
    var availableSubjects = document.getElementById('availableSubjects');
    var chosenSubjects = document.getElementById('chosenSubjects');

    for (var i = 0; i < chosenSubjects.options.length; i++) {
        var option = chosenSubjects.options[i];

        if (option.selected) {
            availableSubjects.options.add(new Option(option.text, option.value));
            chosenSubjects.remove(i);
            i--;
        }
    }
}

function selectChosenSubjects() {
    var chosenSubjects = document.getElementById('chosenSubjects');

    for (var i = 0; i < chosenSubjects.options.length; i++) {
        chosenSubjects.options[i].selected = true;
    }
}