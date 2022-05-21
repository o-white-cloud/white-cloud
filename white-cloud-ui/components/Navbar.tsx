import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import Toolbar from '@mui/material/Toolbar';

import Link from './Link';

interface NavbarProps {
  navLinks: { title: string; path: string }[];
}
const Navbar = (props: NavbarProps) => {
  return (
    <Toolbar
      component="nav"
      sx={{
        display: { xs: `none`, md: `flex` },
      }}
    >
      <Stack direction="row" spacing={4}>
        {props.navLinks.map((l, i) => (
          <Link
            key={`${l.title}${i}`}
            href={l.path}
            variant="button"
            sx={{ color: `white`, opacity: 0.7 }}
          >
            {l.title}
          </Link>
        ))}
        <Button variant='text' color='inherit' href='/login'>Login</Button>
      </Stack>
    </Toolbar>
  );
};

export default Navbar;
