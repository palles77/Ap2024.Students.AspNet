function subjectMoveRight() {
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

function subjectMoveLeft() {
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

    // Get the form that contains the chosenSubjects select box
    var form = chosenSubjects.form;

    // Check if the form is valid
    if (!form.checkValidity()) {
        // If the form is not valid, prevent its submission
        form.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();
        });

        // Add the 'was-validated' class to the form to show validation feedback
        form.classList.add('was-validated');
    }
}