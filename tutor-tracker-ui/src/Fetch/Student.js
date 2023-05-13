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