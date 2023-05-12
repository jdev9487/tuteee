export const addLesson = async (studentId, dateTime, hourlyRate, halfHours, paid) => {
    await fetch(`http://localhost:5293/lessons`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            studentId: studentId,
            dateTime: dateTime,
            hourlyRate: hourlyRate,
            halfHours: halfHours,
            paid: paid
        })
    })
        .then((response) => {
            if (response.ok) {
                response.json().then((data) => {
                });
            } else {
            }
        })
        .catch((error) => {
        });
}