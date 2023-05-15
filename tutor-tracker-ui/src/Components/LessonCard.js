import * as React from 'react';
import { Box, Typography } from "@mui/material";
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import { rateText } from '../Utility/Format';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import PaidIcon from '@mui/icons-material/Paid';
import EditIcon from '@mui/icons-material/Edit';
import IconButton from '@mui/material/IconButton';
import { updateLesson } from '../Fetch/Lesson';
import VerifiedIcon from '@mui/icons-material/Verified';
import CloseIcon from '@mui/icons-material/Close';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

export default function LessonCard(props) {
    const {
        id,
        dateTime,
        hourlyRate,
        duration,
    } = props;

    const [paid, setPaid] = React.useState(props.paid);
    const [editLessonOpen, setEditLessonOpen] = React.useState(false);

    const handleEditLessonOpen = () => {
        setEditLessonOpen(true);
    }
    const handleEditLessonClose = () => {
        setEditLessonOpen(false);
    }

    const handleConfirmEditLesson = (event) => {
        event.preventDefault();
        console.log(event.target[0]);
        console.log(event.target[1]);
        console.log(event.target[2]);
        console.log(event.target[3]);
        return true;
    }

    const getFinish = (start, halfHours) => {
        var numberOfMlSeconds = start.getTime();
        var addMlSeconds = halfHours * 30 * 60 * 1000;
        var newDateObj = new Date(numberOfMlSeconds + addMlSeconds);
        return newDateObj;
    };

    const setLessonAsPaid = () => {
        updateLesson(id, null, null, null, true, () => {}, () => {}, () => {}, (value) => setPaid(value))
    }
    const setLessonAsUnpaid = () => {
        updateLesson(id, null, null, null, false, () => {}, () => {}, () => {}, (value) => setPaid(value))
    }

    return (
        <div>
            <Card sx={{ m: 1, background: '#f0f0f0' }}>
                <CardContent>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center', mb:1 }}>
                        <CalendarMonthIcon sx={{ mr: 2 }} />
                        <Typography>{(new Date(dateTime)).toDateString()}</Typography>
                    </Box>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center', mb:1 }}>
                        <AccessTimeIcon sx={{ mr: 2 }} />
                        <Typography>{(new Date(dateTime)).toLocaleTimeString()} - {getFinish(new Date(dateTime), duration).toLocaleTimeString()}</Typography>
                    </Box>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center', mb:1 }}>
                        <PaidIcon sx={{ mr: 2 }} />
                        <Typography>{rateText(hourlyRate)}/hr</Typography>
                    </Box>
                </CardContent>
                <CardActions sx={{justifyContent: 'end'}}>
                    {paid ? <IconButton onClick={setLessonAsUnpaid}>
                        <VerifiedIcon color='success'/>
                    </IconButton> : <IconButton onClick={setLessonAsPaid}>
                        <CloseIcon color='warning'/>
                    </IconButton>}
                    <IconButton onClick={handleEditLessonOpen}>
                        <EditIcon />
                    </IconButton>
                </CardActions>
            </Card>
            <Dialog open={editLessonOpen} onClose={handleEditLessonClose}>
                <DialogTitle>Edit lesson</DialogTitle>
                <DialogContent>
                    <DialogContentText align='center'>
                        ALL ACTIONS ARE FINAL!
                    </DialogContentText>
                    <form id='edit-lesson-form' onSubmit={handleConfirmEditLesson}>
                        {/* <TextField margin="dense" defaultValue={customerFirstName} id="first-name" label="First name" fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerLastName} id="last-name" label="Last name" fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerEmail} id="email" label="Email" type='email' fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerPhone} id="phone" label="Phone" type='tel' fullWidth variant="standard" /> */}
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleEditLessonClose}>Cancel</Button>
                    <Button type='submit' color='warning' form='edit-lesson-form'>Update</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}