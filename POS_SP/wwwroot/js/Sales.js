const selectors = {
    orderNumber: document.querySelector('#orderNumber'),
    clientId: document.querySelector('#clientId'),
    salesDate: document.querySelector('#salesDate'),
    categoryId: document.querySelector('#categoryId'),
    subCategoryId: document.querySelector('#subCategoryId'),
    brandId: document.querySelector('#brandId'),
    productId: document.querySelector('#productId'),
    unitPrice: document.querySelector('#unitPrice'),
    uom: document.querySelector('#uom'),
    quantity: document.querySelector('#quantity'),
    addProduct: document.querySelector('#addProduct'),
    tableBody: document.querySelector('#tableBody'),
    table: document.querySelector('#table'),
    submitFrom: document.querySelector('#submitFrom'),
    vatPercent: document.querySelector('#vatPercent'),
    vatTaka: document.querySelector('#vatTaka'),
    discountPercent: document.querySelector('#discountPercent'),
    discountTaka: document.querySelector('#discountTaka'),
    taxPercent: document.querySelector('#taxPercent'),
    taxTaka: document.querySelector('#taxTaka'),
    subTotalAmount: document.querySelector('#subTotalAmount'),
    totalAmount: document.querySelector('#totalAmount'),
    productTable: document.querySelector('#productTable'),
    availableQuantity: document.querySelector('#availableQuantity'),
    paymentType: document.querySelector('#paymentType'),
    paymentAmount: document.querySelector('#paymentAmount'),
    downPayment: document.querySelector('#downPayment'),
    dueAmount: document.querySelector('#dueAmount'),
    installmentPeriod: document.querySelector('#installmentPeriod'),
    paymentScheduleTableBody: document.querySelector('#paymentScheduleTableBody'),
    showPaymentSchedule: document.querySelector('#showPaymentSchedule')
};
function clearInputs() {
    selectors.categoryId.options[selectors.categoryId.selectedIndex].value = 0;
    selectors.productId.options[selectors.productId.selectedIndex].value = 0;
    selectors.unitPrice.value = null;
    selectors.uom.value = null;
    selectors.quantity.value = null;
    selectors.clientId.options[selectors.clientId.selectedIndex].value = 0;
    selectors.vatPercent.value = null;
    selectors.vatTaka.value = null;
    selectors.discountPercent.value = null;
    selectors.discountTaka.value = null;
    selectors.taxPercent.value = null;
    selectors.taxTaka.value = null;
}
let orderedProducts = [];
let installments = [];
let subTotalAmount = 0;
let totalAmount = 0;
function getTotalAmount(subTotal, tax, vat, discount) {
    let amount = subTotal;
    let tx = Number(tax);
    let vt = Number(vat);
    let disc = Number(discount);
    if (tx > 0) {
        amount += tx;
    }
    if (vt > 0) {
        amount += vt;
    }
    if (disc > 0) {
        amount -= discount;
    }
    return amount;
}
(function () {
    clearInputs();
    let numberOfProducts = sessionStorage.getItem("numberOfProducts");
    if (numberOfProducts !== null) {
        for (let i = 1; i <= numberOfProducts; i++) {
            let orderedProduct = JSON.parse(sessionStorage.getItem(`item[${i}]`));
            subTotalAmount = subTotalAmount + orderedProduct.individualTotal;
            orderedProducts.push(orderedProduct);
        }
        for (let i = 0; i < numberOfProducts; i++) {
            let addedRow = `<tr id="tableRow[${i + 1}]"><td>
                            ${i + 1}
                        </td>
                        <td>
                            ${orderedProducts[i].category}
                        </td>
                        <td>
                            ${orderedProducts[i].subCategory}
                        </td>
                        <td>
                            ${orderedProducts[i].brand}
                        </td>
                        <td>
                            ${orderedProducts[i].product}
                        </td>
                        <td>
                            ${orderedProducts[i].unitPrice}
                        </td>
                        <td>
                            ${orderedProducts[i].uom}
                        </td>
                        <td>
                            ${orderedProducts[i].quantity}
                        </td>
                        <td>
                            ${orderedProducts[i].individualTotal}
                        </td></tr>`;
            selectors.tableBody.insertAdjacentHTML('beforeend', addedRow);
            selectors.subTotalAmount.innerHTML = subTotalAmount;
            selectors.totalAmount.innerHTML = subTotalAmount;

        }
    }
})();
selectors.addProduct.addEventListener('click', e => {
    e.preventDefault();
    if (selectors.categoryId.options[selectors.categoryId.selectedIndex].value === 0 || selectors.productId.options[selectors.productId.selectedIndex].value === 0 || selectors.unitPrice.value.length === 0 || selectors.uom.value.length === 0 || selectors.quantity.value.length === 0) {
        var x = document.getElementById("snackbar");
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    } else {
        let addedProduct = {
            categoryId: selectors.categoryId.options[selectors.categoryId.selectedIndex].value,
            subCategoryId: selectors.subCategoryId.options[selectors.subCategoryId.selectedIndex].value,
            subCategory: selectors.subCategoryId.options[selectors.subCategoryId.selectedIndex].text,
            brandId: selectors.brandId.options[selectors.brandId.selectedIndex].value,
            brand: selectors.brandId.options[selectors.brandId.selectedIndex].text,
            productId: selectors.productId.options[selectors.productId.selectedIndex].value,
            category: selectors.categoryId.options[selectors.categoryId.selectedIndex].text,
            product: selectors.productId.options[selectors.productId.selectedIndex].text,
            unitPrice: selectors.unitPrice.value,
            uom: selectors.uom.value,
            quantity: selectors.quantity.value,
            availableQuantity: selectors.availableQuantity.value,
            individualTotal: selectors.unitPrice.value * selectors.quantity.value
        };
        let numberOfProducts = sessionStorage.getItem("numberOfProducts");
        if (numberOfProducts === null) {
            sessionStorage.setItem("numberOfProducts", 1);
            numberOfProducts = sessionStorage.getItem("numberOfProducts");
            let addedRow = `<tr id="tableRow[${numberOfProducts}]"><td>
                            ${numberOfProducts}
                        </td>
                        <td>
                            ${addedProduct.category}
                        </td>
                        <td>
                            ${addedProduct.subCategory}
                        </td>
                        <td>
                            ${addedProduct.brand}
                        </td>
                        <td>
                            ${addedProduct.product}
                        </td>
                        <td>
                            ${addedProduct.unitPrice}
                        </td>
                        <td>
                            ${addedProduct.uom}
                        </td>
                        <td>
                            ${addedProduct.quantity}
                        </td>
                        <td>
                            ${addedProduct.individualTotal}
                        </td></tr>`;
            selectors.tableBody.insertAdjacentHTML('beforeend', addedRow);
            sessionStorage.setItem(`item[${numberOfProducts}]`, JSON.stringify(addedProduct));
            orderedProducts.push(addedProduct);
            subTotalAmount += addedProduct.individualTotal;
            selectors.subTotalAmount.innerHTML = subTotalAmount;
            selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);

        } else {
            let dataAvailable = false;
            numberOfProducts++;
            let addedRow = `<tr id="tableRow[${numberOfProducts}]"><td>
                            ${numberOfProducts}
                        </td>
                        <td>
                            ${addedProduct.category}
                        </td>
                        <td>
                            ${addedProduct.subCategory}
                        </td>
                        <td>
                            ${addedProduct.brand}
                        </td>
                        <td>
                            ${addedProduct.product}
                        </td>
                        <td>
                            ${addedProduct.unitPrice}
                        </td>
                        <td>
                            ${addedProduct.uom}
                        </td>
                        <td>
                            ${addedProduct.quantity}
                        </td>
                        <td>
                            ${addedProduct.individualTotal}
                        </td></tr>`;
            for (let i = 0; i < numberOfProducts - 1; i++) {
                let object = JSON.parse(sessionStorage.getItem(`item[${i + 1}]`));
                if (object.categoryId === addedProduct.categoryId && object.productId === addedProduct.productId) {
                    let updatedRow = `<td>
                            ${i + 1}
                        </td>
                        <td>
                            ${addedProduct.category}
                        </td>
                        <td>
                            ${addedProduct.subCategory}
                        </td>
                        <td>
                            ${addedProduct.brand}
                        </td>
                        <td>
                            ${addedProduct.product}
                        </td>
                        <td>
                            ${addedProduct.unitPrice}
                        </td>
                        <td>
                            ${addedProduct.uom}
                        </td>
                        <td>
                            ${addedProduct.quantity}
                        </td>
                        <td>
                            ${addedProduct.individualTotal}
                        </td>`;
                    orderedProducts[i] = addedProduct;
                    sessionStorage.setItem(`item[${i + 1}]`, JSON.stringify(addedProduct));
                    document.getElementById(`tableRow[${i + 1}]`).innerHTML = updatedRow;
                    dataAvailable = true;
                    subTotalAmount = 0;
                    for (let i = 0; i < numberOfProducts - 1; i++) {
                        subTotalAmount += orderedProducts[i].individualTotal;
                    }
                    selectors.subTotalAmount.innerHTML = subTotalAmount;
                    selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
                }
                continue;
            }
            if (dataAvailable === false) {
                orderedProducts.push(addedProduct);
                sessionStorage.setItem("numberOfProducts", numberOfProducts);
                numberOfProducts = sessionStorage.getItem("numberOfProducts");
                selectors.tableBody.insertAdjacentHTML('beforeend', addedRow);
                sessionStorage.setItem(`item[${numberOfProducts}]`, JSON.stringify(addedProduct));
                subTotalAmount += addedProduct.individualTotal;
                selectors.subTotalAmount.innerHTML = subTotalAmount;
                selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
            }
        }
        selectors.categoryId.value = null;
        selectors.productId.value = null;
        selectors.unitPrice.value = null;
        selectors.uom.value = null;
        selectors.quantity.value = null;
        selectors.availableQuantity.value = null;
    }
});
selectors.submitFrom.addEventListener('click', e => {
    e.preventDefault();
    if (orderedProducts.length === 0 || selectors.orderNumber.value.length === 0 || selectors.salesDate.value.length === 0 || selectors.clientId.options[selectors.clientId.selectedIndex].value === 0) {
        var x = document.getElementById("snackbar");
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    } else {
        let postObject = {
            orderRefNo: selectors.orderNumber.value,
            customerId: selectors.clientId.options[selectors.clientId.selectedIndex].value,
            salesDate: selectors.salesDate.value,
            taxAmount: selectors.taxTaka.value,
            taxPercent: selectors.taxPercent.value,
            discountPercent: selectors.discountPercent.value,
            discountAmount: selectors.discountTaka.value,
            vatPercent: selectors.vatPercent.value,
            vatAmount: selectors.vatTaka.value,
            totalAmount: getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value),
            paymentType: selectors.paymentType.options[selectors.paymentType.selectedIndex].text,
            paymentAmount: selectors.paymentAmount.value,
            dueAmount: selectors.dueAmount.value,
            downPayment: selectors.downPayment.value,
            installmentPeriod: selectors.installmentPeriod.options[selectors.installmentPeriod.selectedIndex].value,
            salesDetails: orderedProducts,
            installmentSchedulePayments: installments
        };
        $.ajax({
            type: 'POST',
            url: '/Sales/Create',
            dataType: 'json',
            data: postObject,
            complete: (jqXHR) => {
                if (jqXHR.readyState === 4) {
                    window.location.href = '/Sales';
                    sessionStorage.clear();
                }
            }
        });
    }
});
selectors.categoryId.addEventListener('change', e => {
    let categoryId = selectors.categoryId.options[selectors.categoryId.selectedIndex].value;
    selectors.subCategoryId.value = null;
    selectors.brandId.value = null;
    selectors.productId.value = null;
    selectors.unitPrice.value = null;
    selectors.uom.value = null;
    selectors.quantity.value = null;
    $.ajax({
        type: 'GET',
        url: '/Json/GetSubCategoryList/' + categoryId,
        cache: false,
        dataType: "JSON",
        contentType: "application/x-www-form-urlencoded",
        success: function (response) {
            selectors.subCategoryId.innerHTML = " ";
            for (let i = 0; i < response.length; i++) {
                let option = document.createElement('option');
                option.value = response[i].id;
                option.innerHTML = response[i].name;
                selectors.subCategoryId.appendChild(option);
            }
            selectors.subCategoryId.value = null;
        }
    });
});
selectors.subCategoryId.addEventListener('change', e => {
    let subCategoryId = selectors.subCategoryId.options[selectors.subCategoryId.selectedIndex].value;
    selectors.productId.value = null;
    selectors.unitPrice.value = null;
    selectors.uom.value = null;
    selectors.quantity.value = null;
    $.ajax({
        type: 'GET',
        url: '/Json/GetBrandList/' + subCategoryId,
        cache: false,
        dataType: "JSON",
        contentType: "application/x-www-form-urlencoded",
        success: function (response) {
            selectors.brandId.innerHTML = " ";
            for (let i = 0; i < response.length; i++) {
                let option = document.createElement('option');
                option.value = response[i].id;
                option.innerHTML = response[i].name;
                selectors.brandId.appendChild(option);
            }
            selectors.brandId.value = null;
        }
    });
});
selectors.brandId.addEventListener('change', e => {
    let subCategoryId = selectors.subCategoryId.options[selectors.subCategoryId.selectedIndex].value;
    let brandId = selectors.brandId.options[selectors.brandId.selectedIndex].value;
    selectors.productId.value = null;
    selectors.unitPrice.value = null;
    selectors.uom.value = null;
    selectors.quantity.value = null;
    $.ajax({
        type: 'POST',
        url: '/Json/GetProductList/',
        cache: false,
        data: { subCategoryId: subCategoryId, brandId: brandId },
        dataType: "JSON",
        contentType: "application/x-www-form-urlencoded",
        success: function (response) {
            selectors.productId.innerHTML = " ";
            for (let i = 0; i < response.length; i++) {
                let option = document.createElement('option');
                option.value = response[i].id;
                option.innerHTML = response[i].name;
                selectors.productId.appendChild(option);
            }
            selectors.productId.value = null;
        }
    });
});
selectors.productId.addEventListener('change', e => {
    let productId = selectors.productId.options[selectors.productId.selectedIndex].value;
    selectors.unitPrice.value = null;
    selectors.uom.value = null;
    selectors.quantity.value = null;
    $.ajax({
        type: 'GET',
        url: '/Json/GetProductDetail/' + productId,
        cache: false,
        dataType: "JSON",
        contentType: "application/x-www-form-urlencoded",
        success: function (response) {
            selectors.unitPrice.value = response.unitPrice;
            selectors.uom.value = response.uom;
        }
    });
    $.ajax({
        type: "GET",
        url: '/Json/GetAvailableQuantity/' + productId,
        cache: false,
        dataType: "JSON",
        contentType: "application/x-www-form-urlencoded",
        success: function (response) {
            selectors.availableQuantity.value = response.quantity;
        }
    });
});
selectors.taxPercent.addEventListener('change', e => {
    e.preventDefault();
    if (subTotalAmount > 0) {
        let taxPercent = selectors.taxPercent.value;
        selectors.taxTaka.value = (subTotalAmount * taxPercent) / 100;
        selectors.taxTaka.disabled = true;
        selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
    }
});
selectors.vatPercent.addEventListener('change', e => {
    e.preventDefault();
    if (subTotalAmount > 0) {
        let vatPercent = selectors.vatPercent.value;
        selectors.vatTaka.value = (subTotalAmount * vatPercent) / 100;
        selectors.vatTaka.disabled = true;
        selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
    }
});
selectors.discountPercent.addEventListener('change', e => {
    e.preventDefault();
    if (subTotalAmount > 0) {
        let discountPercent = selectors.discountPercent.value;
        selectors.discountTaka.value = (subTotalAmount * discountPercent) / 100;
        selectors.discountTaka.disabled = true;
        selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
    }
});
selectors.discountTaka.addEventListener('change', e => {
    e.preventDefault();
    if (subTotalAmount > 0) {
        selectors.discountPercent.disabled = true;
        selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
    }
});
selectors.taxTaka.addEventListener('change', e => {
    e.preventDefault();
    if (subTotalAmount > 0) {
        selectors.taxPercent.disabled = true;
        selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
    }
});
selectors.vatTaka.addEventListener('change', e => {
    e.preventDefault();
    if (subTotalAmount > 0) {
        selectors.vatPercent.disabled = true;
        selectors.totalAmount.innerHTML = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
    }
});
selectors.paymentAmount.addEventListener('change', e => {
    e.preventDefault();
    selectors.dueAmount.value = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value) - Number(selectors.paymentAmount.value);
});
selectors.paymentType.addEventListener('change', e => {
    e.preventDefault();
    let paymentType = selectors.paymentType.options[selectors.paymentType.selectedIndex].text;
    if (paymentType === "EMI") {
        $('#paymentAmountGroup').hide();
        $('#dueAmountGroup').hide();
        $('#downPaymentAmountGroup').show();
        $('#installmentPeriodGroup').show();
        $('#showPaymentScheduleGroup').show();
    }
});
selectors.installmentPeriod.addEventListener('change', e => {
    installments = [];
    selectors.paymentScheduleTableBody.innerHTML = " ";
    let period = selectors.installmentPeriod.options[selectors.installmentPeriod.selectedIndex].value;
    let totalAmount = getTotalAmount(subTotalAmount, selectors.taxTaka.value, selectors.vatTaka.value, selectors.discountTaka.value);
    let scheduleAmount = (Number(totalAmount) - Number(selectors.downPayment.value)) / period;
    let date = new Date();
    for (var i = 1; i <= period; i++) {
        let day = ("0" + date.getDate(date.setDate(10))).slice(-2);
        var month = ("0" + (date.getMonth() + (i+1))).slice(-2);
        var scheduleDate = date.getFullYear() + "-" + month + "-" + day;
        let installment = {
            scheduleDate: scheduleDate,
            scheduleAmount: Math.ceil(scheduleAmount)
        };
        let tableRow = `<tr>
                            <td>${i}</td>
                            <td>${installment.scheduleDate}</td>
                            <td>${installment.scheduleAmount}</td>
                        </tr>`;
        selectors.paymentScheduleTableBody.insertAdjacentHTML('beforeend', tableRow);
        installments.push(installment);
    }
});
selectors.downPayment.addEventListener('change', e => {
    e.preventDefault();
    let downPayment = selectors.downPayment.value;    
    if (downPayment > 0) {
        selectors.installmentPeriod.disabled = false;
    }
});