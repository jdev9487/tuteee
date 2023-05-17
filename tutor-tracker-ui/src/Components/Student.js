import React, { useEffect } from 'react';
import { Link, NavLink, useParams } from 'react-router-dom';
import { getLessonsAssociatedWithStudent, getStudent } from '../Fetch/Student';
import Grid from '@mui/material/Grid';
import LessonCard from './LessonCard';
import { Box, Typography } from '@mui/material';

export default function Student() {

    const [lessons, setLessons] = React.useState([]);
    const [name, setName] = React.useState('');

    const { id } = useParams();
    useEffect(() => {
        getLessonsAssociatedWithStudent(id, (data) => setLessons(data));
        getStudent(id, (name) => setName(name));
    }, []);

    return (
        <div>
            <Typography variant='h2' m={2}>
                {name}
            </Typography>
            <Grid container spacing={2}>
                {lessons.map((x, i) => {
                    return (
                        <Grid item xs={4} key={i}>
                            <LessonCard id={x.id} dateTime={x.dateTime} hourlyRate={x.hourlyRate} duration={x.halfHours} paid={x.paid} />
                        </Grid>
                    );
                })}
            </Grid>
        </div>
    );
}