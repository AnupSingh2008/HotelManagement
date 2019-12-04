function add_number() {
    debugger;
    var first_number = parseInt(document.getElementById("NoOfDays").value);
    var second_number = parseInt(document.getElementById("TotalAmount").value);
    var result = first_number * second_number;

    document.getElementById("TextBoxTotalAmount").value = result;
}