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

export default function LessonCard(props) {
    const {
        id,
        dateTime,
        hourlyRate,
        duration,
    } = props;

    const [paid, setPaid] = React.useState(props.paid);

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
                    <IconButton>
                        <EditIcon />
                    </IconButton>
                </CardActions>
            </Card>
        </div>
    );
}