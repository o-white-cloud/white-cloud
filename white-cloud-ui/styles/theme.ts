import { pink, teal } from '@mui/material/colors';
import { createTheme, responsiveFontSizes } from '@mui/material/styles';

// Create a theme instance.
let theme = createTheme({
  palette: {
    primary: teal,
    secondary: pink,
  },
});

theme = responsiveFontSizes(theme);

export default theme;