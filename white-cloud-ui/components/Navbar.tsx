import { Roles } from 'models/User';
import { useRouter } from 'next/router';
import { useCallback } from 'react';

import LogoutIcon from '@mui/icons-material/Logout';
import PersonIcon from '@mui/icons-material/Person';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import Toolbar from '@mui/material/Toolbar';

import { useUser } from './hooks';
import Link from './Link';

interface NavbarProps {
  navLinks: { title: string; path: string }[];
}
const Navbar = (props: NavbarProps) => {
  const [user, logout] = useUser();
  const router = useRouter();
  const onLogout = useCallback(() => {
    fetch(`${process.env.NEXT_PUBLIC_HOST}/authentication/logout`).then((_) => {
      logout();
      router.push('/');
    });
  }, [router, logout]);

  return (
    <Toolbar
      component="nav"
      sx={{
        display: { xs: `none`, md: `flex` },
      }}
    >
      <Stack direction="row" spacing={4} sx={{ alignItems: 'center' }}>
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
        {user.authenticated ? (
          <>
            <Link
              href={
                user.roles.includes(Roles.Therapist) ? '/therapist' : '/user'
              }
              variant="button"
              sx={{ color: `white`, opacity: 0.7 }}
            >
              {`${user.firstName} ${user.lastName}`}
            </Link>
            <Button color="inherit" onClick={onLogout} startIcon={<LogoutIcon/>}>
              Logout
            </Button>
          </>
        ) : (
          <Button variant="text" color="inherit" href="/login">
            Login
          </Button>
        )}
      </Stack>
    </Toolbar>
  );
};

export default Navbar;
