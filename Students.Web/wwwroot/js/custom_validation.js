$(document).ready(function () {
    $('form').find('input').each(function () {
        $(this).on('keyup', function () {
            $(this).valid();
        });
    });

    $.validator.addMethod('capital-letters-only', function (value, element) {
        return this.optional(element) || /^[A-Z\s]+$/.test(value);
    }, '');

    $.validator.unobtrusive.adapters.add('capital-letters-only', [], function (options) {
        options.rules['capital-letters-only'] = {};
        options.messages['capital-letters-only'] = options.message;
    });

    $.validator.addMethod('small-letters-only', function (value, element) {
        return this.optional(element) || /^[a-z\s]+$/.test(value);
    }, '');

    $.validator.unobtrusive.adapters.add('small-letters-only', [], function (options) {
        options.rules['small-letters-only'] = {};
        options.messages['small-letters-only'] = options.message;
    });
});