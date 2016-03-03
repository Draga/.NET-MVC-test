$(document).ready(function () {
    // Toggles visibility of checkboxes with class labelClickHides when their label is clicked.
    $("input.labelClickHides:checkbox").each(function (index, checkbox) {
        $("label[for='" + $(checkbox).attr("id") + "']").click(function() {
            $(checkbox).toggle();
        });
    });
})