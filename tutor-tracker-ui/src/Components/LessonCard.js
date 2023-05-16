import * as React from 'react';
import { Box, DialogContentText, TextField, Typography } from "@mui/material";
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import { halfHourText, rateText } from '../Utility/Format';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import PaidIcon from '@mui/icons-material/Paid';
import Tooltip from '@mui/material/Tooltip';
import EditIcon from '@mui/icons-material/Edit';
import IconButton from '@mui/material/IconButton';
import { updateLesson, deleteLesson } from '../Fetch/Lesson';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DeleteIcon from '@mui/icons-material/DeleteOutline';
import DialogTitle from '@mui/material/DialogTitle';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import dayjs from 'dayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { MobileDateTimePicker } from '@mui/x-date-pickers/MobileDateTimePicker';
import Slider from '@mui/material/Slider';
import FormControlLabel from '@mui/material/FormControlLabel';
import Switch from '@mui/material/Switch';
import InputLabel from '@mui/material/InputLabel';

export default function LessonCard(props) {
    const {
        id,
    } = props;

    const [paid, setPaid] = React.useState(props.paid);
    const [dateTime, setDateTime] = React.useState(props.dateTime);
    const [hourlyRate, setHourlyRate] = React.useState(props.hourlyRate);
    const [duration, setDuration] = React.useState(props.duration);
    const [editLessonOpen, setEditLessonOpen] = React.useState(false);
    const [deleteLessonOpen, setDeleteLessonOpen] = React.useState(false);
    const [deleteButtonDisabled, setDeleteButtonDisabled] = React.useState(true);

    const handleEditLessonOpen = () => {
        setEditLessonOpen(true);
    }
    const handleEditLessonClose = () => {
        setEditLessonOpen(false);
    }
    const handleDeleteLessonOpen = () => {
        setDeleteLessonOpen(true);
    }
    const handleDeleteLessonClose = () => {
        setDeleteLessonOpen(false);
        setDeleteButtonDisabled(true);
    }
    const handleConfirmDeleteChange = (event) => {
        if (event.target.value === 'DELETE') {
            setDeleteButtonDisabled(false);
        } else {
            setDeleteButtonDisabled(true);
        }
    }

    const handleConfirmEditLesson = (event) => {
        event.preventDefault();
        const newDateTime = new Date(event.target[0].value).toISOString();
        const newHalfHours = event.target[2].value;
        const newHourlyRate = event.target[3].value;
        const newPaid = event.target[4].checked;
        updateLesson(id, newDateTime, newHourlyRate, newHalfHours, newPaid, (dt) => setDateTime(dt), (hr) => setHourlyRate(hr), (hh) => setDuration(hh), (p) => setPaid(p))
        setEditLessonOpen(false);
    };

    const handleDeleteLessonConfirm = () => {
        deleteLesson(id);
        setDeleteLessonOpen(false);
        window.location.reload(false);
    };

    const getFinish = (start, halfHours) => {
        var numberOfMlSeconds = start.getTime();
        var addMlSeconds = halfHours * 30 * 60 * 1000;
        var newDateObj = new Date(numberOfMlSeconds + addMlSeconds);
        return newDateObj;
    };

    return (
        <div>
            <Card sx={{ m: 1, background: '#f0f0f0' }}>
                <CardContent>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center', mb: 1 }}>
                        <CalendarMonthIcon sx={{ mr: 2 }} />
                        <Typography>{(new Date(dateTime)).toDateString()}</Typography>
                    </Box>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center', mb: 1 }}>
                        <AccessTimeIcon sx={{ mr: 2 }} />
                        <Typography>{(new Date(dateTime)).toLocaleTimeString()} - {getFinish(new Date(dateTime), duration).toLocaleTimeString()}</Typography>
                    </Box>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center', mb: 1 }}>
                        <PaidIcon color={paid ? 'success' : 'warning'} sx={{ mr: 2 }} />
                        <Typography sx={{mr:2}}>{rateText(hourlyRate)}/hr</Typography>
                    </Box>
                </CardContent>
                <CardActions sx={{ justifyContent: 'end' }}>
                    <Tooltip title="Edit" placement='top'>
                        <IconButton onClick={handleEditLessonOpen}>
                            <EditIcon color='info'/>
                        </IconButton>
                    </Tooltip>
                    <Tooltip title="Delete" placement='top'>
                        <IconButton onClick={handleDeleteLessonOpen}>
                            <DeleteIcon color='error'/>
                        </IconButton>
                    </Tooltip>
                </CardActions>
            </Card>
            <Dialog open={editLessonOpen} fullWidth={true} onClose={handleEditLessonClose}>
                <DialogTitle>Edit lesson</DialogTitle>
                <DialogContent>
                    <form id='edit-lesson-form' onSubmit={handleConfirmEditLesson}>
                        <InputLabel id="edit-lesson-datetime">Date & Time</InputLabel>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <DemoContainer components={['DatePicker']}>
                                <MobileDateTimePicker defaultValue={dayjs(dateTime)} sx={{ mb: 2 }} labelId="edit-lesson-datetime" />
                            </DemoContainer>
                        </LocalizationProvider>
                        <InputLabel id="edit-lesson-duration-label">Duration</InputLabel>
                        <Slider
                            sx={{ mb: 2 }}
                            labelId="edit-lesson-duration-label"
                            defaultValue={duration}
                            valueLabelFormat={halfHourText}
                            valueLabelDisplay="auto"
                            step={1}
                            marks
                            min={0}
                            max={6}
                        />
                        <InputLabel id="edit-lesson-rate-label">Hourly rate</InputLabel>
                        <Slider
                            sx={{ mb: 2 }}
                            labelId="edit-lesson-rate-label"
                            defaultValue={hourlyRate}
                            valueLabelFormat={rateText}
                            valueLabelDisplay="auto"
                            step={5}
                            marks
                            min={5}
                            max={100}
                        />
                        <FormControlLabel control={<Switch defaultChecked={paid}/>} label="Paid" />
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleEditLessonClose}>Cancel</Button>
                    <Button type='submit' color='warning' form='edit-lesson-form'>Update</Button>
                </DialogActions>
            </Dialog>
            <Dialog open={deleteLessonOpen} fullWidth={true} onClose={handleDeleteLessonClose}>
                <DialogTitle>Delete lesson</DialogTitle>
                <DialogContent>
                    <DialogContentText align='center' color='error'>
                        WARNING: THIS IS A PERMANENT ACTION!
                    </DialogContentText>
                    <Box mt={2} sx={{display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
                        <Typography mr={1} align='center'>
                            Type 'DELETE' to confirm:
                        </Typography>
                        <TextField onChange={handleConfirmDeleteChange} margin="dense" size='small' id="confirm-delete" variant="outlined" />
                    </Box>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleDeleteLessonClose}>Cancel</Button>
                    <Button onClick={handleDeleteLessonConfirm} disabled={deleteButtonDisabled} variant='contained' color='error'>Delete</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}