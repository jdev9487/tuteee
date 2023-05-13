import React, { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getLessonsAssociatedWithStudent } from '../Fetch/Student';
import Grid from '@mui/material/Grid';
import LessonCard from './LessonCard';

export default function Student() {

    const [lessons, setLessons] = React.useState([]);

    const { id } = useParams();
    useEffect(() => {
        getLessonsAssociatedWithStudent(id, (data) => setLessons(data))
    }, []);

    return (
        <div>
            <Grid container spacing={2}>
                {lessons.map(x => {
                    return (
                        <Grid item xs={4}>
                            <LessonCard id={x.id} dateTime={x.dateTime} hourlyRate={x.hourlyRate} duration={x.halfHours} paid={x.paid} />
                        </Grid>
                    );
                })}
            </Grid>
        </div>
    );
}