export const getLessonsAssociatedWithStudent = async (studentId, setLessons) => {
    await fetch(`http://localhost:5293/students/${studentId}/lessons`, {
    })
        .then((response) => {
            if (response.ok) {
                response.json().then((data) => {
                    setLessons(data);
                });
            } else {
            }
        })
        .catch((error) => {
        });
}

export const getStudent = async (studentId, setName) => {
    await fetch(`http://localhost:5293/students/${studentId}`, {
    })
        .then((response) => {
            if (response.ok) {
                response.json().then((data) => {
                    setName(`${data.firstName} ${data.lastName}`);
                });
            } else {
            }
        })
        .catch((error) => {
        });
}

export const addStudent = async (invoiceeId, firstName, lastName, setStudents) => {
    await fetch(`http://localhost:5293/students`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            invoiceeId: invoiceeId,
            firstName: firstName,
            lastName: lastName
        })
    })
        .then((response) => {
            if (response.ok) {
                response.json().then((data) => {
                    setStudents(firstName, lastName, data.id)
                });
            } else {
            }
        })
        .catch((error) => {
        });
}