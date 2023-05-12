import React, { useEffect } from 'react';
import { useParams } from 'react-router-dom';

export default function Student() {

    const {id} = useParams();

    return (
        <div>
            {id}
        </div>
    );
}