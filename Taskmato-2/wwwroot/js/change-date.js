function incrementDate(date) {
    document.getElementById('date').innerHTML = date.getDate() + 1;
};

function decrementDate(date) {
    document.getElementById('date').innerHTML = date.getDate() - 1;
}