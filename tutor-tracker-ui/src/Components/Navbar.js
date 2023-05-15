import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

function Navbar() {
    return (
        <AppBar position="static">
            <Container maxWidth="xl">
                <Toolbar disableGutters>
                    <Typography
                        variant="h6"
                        noWrap
                        component="a"
                        href="/"
                        sx={{
                            mr: 2,
                            flexGrow: 1,
                            display: { xs: 'flex' },
                            fontWeight: 700,
                            color: 'inherit',
                            textDecoration: 'none',
                        }}>
                        Tutor Tracker
                    </Typography>
                    <Box sx={{ flexGrow: 0, ml: 8 }}>
                        <IconButton sx={{ p: 0 }}>
                            <AccountCircleIcon fontSize='large' />
                        </IconButton>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
}
export default Navbar;