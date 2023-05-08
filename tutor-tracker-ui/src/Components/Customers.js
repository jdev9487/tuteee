import React, { useEffect } from 'react';
import CustomerCard from './CustomerCard';
import { getCustomers } from '../Fetch/Customer'

export default function Customers() {

    const [customers, setCustomers] = React.useState([]);

    useEffect(() => {
        getCustomers((data) => setCustomers(data))
    }, []);

    return (
        <div>
            {customers.map((x, i) => {
                return (<CustomerCard
                    key={i}
                    id={x.id}
                    firstName={x.firstName}
                    lastName={x.lastName}
                    email={x.email}
                    phone={x.phone}
                    students={x.students} />);
            })}
        </div>
    );
}