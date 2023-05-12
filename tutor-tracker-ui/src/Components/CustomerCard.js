import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import EmailIcon from '@mui/icons-material/Email';
import { Box, IconButton } from '@mui/material';
import ReceiptIcon from '@mui/icons-material/Receipt';
import Divider from '@mui/material/Divider';
import InputLabel from '@mui/material/InputLabel';
import EditIcon from '@mui/icons-material/Edit';
import PostAddIcon from '@mui/icons-material/PostAdd';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import TextField from '@mui/material/TextField';
import { NavLink } from "react-router-dom";
import Dialog from '@mui/material/Dialog';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { updateCustomer } from '../Fetch/Customer';
import { addLesson } from '../Fetch/Lesson';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { MobileDateTimePicker } from '@mui/x-date-pickers/MobileDateTimePicker';
import Slider from '@mui/material/Slider';
import FormControlLabel from '@mui/material/FormControlLabel';
import Switch from '@mui/material/Switch';
import { EventNote } from '@mui/icons-material';

function halfHourText(value) {
    const hours = String(Math.floor(value / 2)).padStart(2, '0');
    const mins = String((value % 2) * 30).padStart(2, '0');
    
    const text = `${hours}:${mins}`;
    return text;
}

function rateText(value) {
    return `£ ${value}`;
}

export default function CustomerCard(props) {
    const [editCustomerOpen, setEditCustomerOpen] = React.useState(false);
    const [addLessonOpen, setAddLessonOpen] = React.useState(false);
    const [customerFirstName, setCustomerFirstName] = React.useState(props.firstName)
    const [customerLastName, setCustomerLastName] = React.useState(props.lastName)
    const [customerPhone, setCustomerPhone] = React.useState(props.phone)
    const [customerEmail, setCustomerEmail] = React.useState(props.email)
    const [studentToAddLessonFor, setStudentToAddLessonFor] = React.useState(props.students[0])

    const handleEditCustomerOpen = () => {
        setEditCustomerOpen(true);
    };
    const handleEditCustomerClose = () => {
        setEditCustomerOpen(false);
    };

    const handleAddLessonOpen = () => {
        setAddLessonOpen(true);
    }
    const handleAddLessonClose = () => {
        setAddLessonOpen(false);
    }
    const handleSetStudentToAddLessonFor = (event) => {
        setStudentToAddLessonFor(event.target.value);
    }

    const handleConfirmEditCustomer = (event) => {
        event.preventDefault();
        const firstName = event.target[0].value;
        const lastName = event.target[1].value;
        const email = event.target[2].value;
        const phone = event.target[3].value;
        updateCustomer(props.id, firstName, lastName, email, phone,
            (fn) => setCustomerFirstName(fn),
            (ln) => setCustomerLastName(ln),
            (e) => setCustomerEmail(e),
            (p) => setCustomerPhone(p));
        setEditCustomerOpen(false);
        return true;
    };
    const handleConfirmAddLesson = (event) => {
        event.preventDefault();
        const dateTime = new Date(event.target[2].value).toISOString();
        const halfHours = event.target[4].value;
        const rate = event.target[5].value;
        const paid = event.target[6].checked;
        addLesson(studentToAddLessonFor.id, dateTime, rate, halfHours, paid);
        setAddLessonOpen(false);
    }

    return (
        <div>
            <Card sx={{ m: 1, background: '#f0f0f0' }}>
                <CardContent>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center' }}>
                        <Box sx={{ flexGrow: 1, display: 'flex', flexDirection: 'row', alignItems: 'center' }}>
                            <Typography mr={1} align='left' variant="h5" component="div">
                                {customerFirstName} {customerLastName}
                            </Typography>
                            <IconButton onClick={handleEditCustomerOpen}>
                                <EditIcon />
                            </IconButton>
                            <IconButton onClick={handleAddLessonOpen}>
                                <PostAddIcon />
                            </IconButton>
                            <IconButton>
                                <PersonAddIcon />
                            </IconButton>
                        </Box>
                        <Typography variant="h6" color='text.secondary' align='right' component="div">
                            {customerPhone}
                        </Typography>
                        <Divider sx={{ mx: 1 }} orientation="vertical" flexItem />
                        <Typography variant="h6" color='text.secondary' align='right' component="div">
                            {customerEmail}
                        </Typography>
                    </Box>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        {[].concat(...props.students.map((x, i) => [<Typography key={i} variant='h6'>
                            <NavLink to={`/student/${x.id}`}>
                                {x.firstName} {x.lastName}
                            </NavLink>
                        </Typography>, <Divider key={i} sx={{ mx: 1 }} orientation="vertical" flexItem />])).slice(0, -1)}
                    </Box>
                </CardContent>
                <CardActions>
                    <Button variant="outlined" startIcon={<EmailIcon />}>
                        Contact
                    </Button>
                    <Button color='success' variant="outlined" startIcon={<ReceiptIcon />}>
                        Invoice
                    </Button>
                </CardActions>
            </Card>
            <Dialog open={editCustomerOpen} onClose={handleEditCustomerClose}>
                <DialogTitle>Edit {customerFirstName} {customerLastName}</DialogTitle>
                <DialogContent>
                    <DialogContentText align='center'>
                        ALL ACTIONS ARE FINAL!
                    </DialogContentText>
                    <form id='edit-customer-form' onSubmit={handleConfirmEditCustomer}>
                        <TextField margin="dense" defaultValue={customerFirstName} id="first-name" label="First name" fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerLastName} id="last-name" label="Last name" fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerEmail} id="email" label="Email" type='email' fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerPhone} id="phone" label="Phone" type='tel' fullWidth variant="standard" />
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleEditCustomerClose}>Cancel</Button>
                    <Button type='submit' color='warning' form='edit-customer-form'>Update</Button>
                </DialogActions>
            </Dialog>
            <Dialog open={addLessonOpen} onClose={handleAddLessonClose}>
                <DialogTitle>Add lesson for {customerFirstName} {customerLastName}</DialogTitle>
                <DialogContent>
                    <form id='add-lesson' onSubmit={handleConfirmAddLesson}>
                        <InputLabel id="select-student-for-add-lesson-label">Student</InputLabel>
                        <Select
                            sx={{mb:2}}
                            labelId="select-student-for-add-lesson-label"
                            id="select-student-for-add-lesson"
                            onChange={handleSetStudentToAddLessonFor}>
                            {props.students.map(x => {
                                return (
                                    <MenuItem value={x}>{x.firstName} {x.lastName}</MenuItem>
                                )
                            })}
                        </Select>
                        <InputLabel id="add-lesson-datetime">Date & Time</InputLabel>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <DemoContainer components={['DatePicker']}>
                                <MobileDateTimePicker sx={{mb:2}} labelId="add-lesson-datetime"/>
                            </DemoContainer>
                        </LocalizationProvider>
                        <InputLabel id="add-lesson-duration-label">Duration</InputLabel>
                        <Slider
                            sx={{mb:2}}
                            labelId="add-lesson-duration-label"
                            defaultValue={2}
                            valueLabelFormat={halfHourText}
                            valueLabelDisplay="auto"
                            step={1}
                            marks
                            min={0}
                            max={6}
                        />
                        <InputLabel id="add-lesson-rate-label">Hourly rate</InputLabel>
                        <Slider
                            sx={{mb:2}}
                            labelId="add-lesson-rate-label"
                            defaultValue={60}
                            valueLabelFormat={rateText}
                            valueLabelDisplay="auto"
                            step={5}
                            marks
                            min={5}
                            max={100}
                        />
                        <FormControlLabel control={<Switch />} label="Paid" />
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleAddLessonClose}>Cancel</Button>
                    <Button type='submit' color='warning' form='add-lesson'>Add lesson</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}