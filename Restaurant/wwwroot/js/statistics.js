document.addEventListener("DOMContentLoaded", function () {
    var revenueCtx = document.getElementById('revenueChart').getContext('2d');
    new Chart(revenueCtx, {
        type: 'line',
        data: {
            labels: revenueLabels,
            datasets: [{
                label: 'Total Revenue ($)',
                data: revenueData,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 2,
                fill: true
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    var dishesCtx = document.getElementById('topDishesChart').getContext('2d');
    new Chart(dishesCtx, {
        type: 'bar',
        data: {
            labels: topDishesLabels,
            datasets: [{
                label: 'Orders',
                data: topDishesData,
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    var categoryCtx = document.getElementById('categoryChart').getContext('2d');
    new Chart(categoryCtx, {
        type: 'pie',
        data: {
            labels: categoryLabels,
            datasets: [{
                data: categoryData,
                backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4CAF50', '#9966FF']
            }]
        }
    });

    var userEmployeeCtx = document.getElementById('userEmployeeGrowthChart').getContext('2d');
    new Chart(userEmployeeCtx, {
        type: 'line',
        data: {
            labels: userEmployeeGrowthLabels,
            datasets: [
                {
                    label: 'Users',
                    data: userGrowthData,
                    backgroundColor: 'rgba(153, 102, 255, 0.2)',
                    borderColor: 'rgba(153, 102, 255, 1)',
                    borderWidth: 2,
                },
                {
                    label: 'Employees',
                    data: employeeGrowthData,
                    backgroundColor: 'rgba(255, 159, 64, 0.2)',
                    borderColor: 'rgba(255, 159, 64, 1)',
                    borderWidth: 2
                }
            ]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        stepSize: 1
                    }
                }
            }
        }
    });
});

