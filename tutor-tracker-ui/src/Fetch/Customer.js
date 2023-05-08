export const getCustomers = async (setCustomers) => {
    await fetch(`http://localhost:5293/customers`, {
    })
        .then((response) => {
            if (response.ok) {
                response.json().then((data) => {
                    setCustomers(data);
                });
            } else {
            }
        })
        .catch((error) => {
        });
}

export const updateCustomer = async (id, newFirstName, newLastName, newEmail, newPhone, setCustomerFirstName, setCustomerLastName, setCustomerEmail, setCustomerPhone) => {
    await fetch(`http://localhost:5293/customers`, {
        method: 'PATCH',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            id: id,
            firstName: newFirstName,
            lastName: newLastName,
            phone: newPhone,
            email: newEmail
        })
    })
        .then((response) => {
            if (response.ok) {
                response.json().then((data) => {
                    console.log(data);
                    setCustomerFirstName(data.firstName);
                    setCustomerLastName(data.lastName);
                    setCustomerPhone(data.phone);
                    setCustomerEmail(data.email);
                });
            } else {
            }
        })
        .catch((error) => {
        });
}