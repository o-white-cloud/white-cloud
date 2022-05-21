import Typography from '@mui/material/Typography';

import Link from './Link';

export const Copyright: React.FC<{}> = (props: any) => {
    return (
      <Typography
        variant="body2"
        color="text.secondary"
        align="center"
        {...props}
      >
        {'Copyright © '}
        <Link color="inherit" href="https://white-cloud.ro/">
          white-cloud.ro
        </Link>{' '}
        {new Date().getFullYear()}
        {'.'}
      </Typography>
    );
  }