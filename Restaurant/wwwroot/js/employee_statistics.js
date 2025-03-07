document.addEventListener("DOMContentLoaded", function () {
    var ctx1 = document.getElementById('orderChart').getContext('2d');
    new Chart(ctx1, {
        type: 'bar',
        data: {
            labels: ["Total Orders", "Completed Orders", "Pending Orders"],
            datasets: [{
                label: 'Order Stats',
                data: orderData,
                backgroundColor: ['#007bff', '#28a745', '#ffc107']
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });

    var ctx2 = document.getElementById('incomeChart').getContext('2d');
    new Chart(ctx2, {
        type: 'pie',
        data: {
            labels: ["Income", "Tips"],
            datasets: [{
                data: incomeData,
                backgroundColor: ['#FF6384', '#36A2EB']
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
});
