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

export const updateLesson = async (id, dateTime, hourlyRate, halfHours, paid, setDateTime, setHourlyRate, setHalfHours, setPaid) => {
    await fetch(`http://localhost:5293/lessons`, {
        method: 'PATCH',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            lessonId: id,
            dateTime: dateTime,
            hourlyRate: hourlyRate,
            halfHours: halfHours,
            paid: paid
        })
    })
        .then((response) => {
            if (response.ok) {
                response.json().then((data) => {
                    setDateTime(data.dateTime);
                    setHourlyRate(data.hourlyRate);
                    setHalfHours(data.halfHours);
                    setPaid(data.paid);
                });
            } else {
            }
        })
        .catch((error) => {
        });
}