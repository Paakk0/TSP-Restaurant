function toggleDetails(orderId) {
    var details = document.getElementById("orderDetails-" + orderId);
    details.classList.toggle("show");
}