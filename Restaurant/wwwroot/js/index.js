function toggleCurrentMenu(button) {
    const category = button.closest('.category');
    const productList = category.querySelector('.product-list');

    document.querySelectorAll('.category').forEach(cat => {
        if (cat !== category) {
            cat.classList.remove('expanded');
            cat.querySelector('.product-list').classList.add('hidden');
        }
    });

    productList.classList.toggle('hidden');
    category.classList.toggle('expanded');
}

function toggleAllMenus(button) {
    const categories = document.getElementsByClassName("category");
    for (let category of categories) {
        const productList = category.querySelector('.product-list');
        productList.classList.toggle('hidden');

        if (productList.classList.contains('hidden')) {
            button.textContent = 'Expand All';
        } else {
            button.textContent = 'Collapse All';
        }
    }
}

function toggleTableNumber() {
    var place = document.getElementById("place").value;
    var tableContainer = document.getElementById("table-number-container");
    var tableNumber = document.getElementById("table-number");

    if (place === "table") {
        tableContainer.style.display = "block";
        tableNumber.required = true;
    } else {
        tableContainer.style.display = "none";
        tableNumber.required = false;
    }
}

function toggleQuantityInput(checkbox) {
    var quantityInput = checkbox.closest('label').nextElementSibling;
    if (checkbox.checked) {
        quantityInput.style.display = 'inline-block';
    } else {
        quantityInput.style.display = 'none';
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const tableRadio = document.getElementById("table");
    const homeRadio = document.getElementById("home");
    const tableNumberContainer = document.getElementById("table-number-container");

    function handlePlaceSelection() {
        if (tableRadio.checked) {
            tableNumberContainer.style.display = "block";
        } else {
            tableNumberContainer.style.display = "none";
        }
    }

    tableRadio.addEventListener("change", handlePlaceSelection);
    homeRadio.addEventListener("change", handlePlaceSelection);

    handlePlaceSelection();
});
