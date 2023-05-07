import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import EmailIcon from '@mui/icons-material/Email';
import { Box } from '@mui/material';
import ReceiptIcon from '@mui/icons-material/Receipt';
import Divider from '@mui/material/Divider';

export default function CustomerCard(props) {
    return (
        <Card sx={{m: 1, background: '#f0f0f0'}}>
            <CardContent>
                <Box sx={{display: 'flex', flexDirection: 'row'}}>
                    <Typography sx={{flexGrow: 1}} align='left' variant="h5" component="div">
                        {props.name}
                    </Typography>
                    <Typography variant="h6" color='text.secondary' align='right' component="div">
                        {props.phone}
                    </Typography>
                    <Divider sx={{mx:1}} orientation="vertical" flexItem />
                    <Typography variant="h6" color='text.secondary' align='right' component="div">
                        {props.email}
                    </Typography>
                </Box>
                <Box sx={{display: 'flex', alignItems: 'center'}}>
                    {[].concat(...props.students.map(x => [<Typography variant='h6'>
                        {x}
                    </Typography>, <Divider sx={{mx:1}} orientation="vertical" flexItem />])).slice(0, -1)}
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
    );
}