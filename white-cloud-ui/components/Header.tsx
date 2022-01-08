import Image from 'next/image';

import AppBar from '@mui/material/AppBar';
import Container from '@mui/material/Container';
import IconButton from '@mui/material/IconButton';
import Toolbar from '@mui/material/Toolbar';
import { styled } from '@mui/system';

import Link from './Link';
import Navbar from './Navbar';

const Offset = styled('div')(({ theme }) => ({
  minHeight: 56,
  [`${theme.breakpoints.up('xs')} and (orientation: landscape)`]: {
    minHeight: 48,
  },
  [theme.breakpoints.up('sm')]: {
    minHeight: 64,
  },
}));

export const navLinks = [
    { title: `home`, path: `/` },
    { title: `about us`, path: `/about-us` },
    // { title: `menu`, path: `/menu` },
    { title: `contact`, path: `/contact` },
  ];

const Header = () => {
  return (
    <>
      <AppBar position="fixed">
        <Toolbar>
          <Container
            
            sx={{ display: `flex`, justifyContent: `space-between` }}
          >
            {/* <Link activeClassName="active" href="/">
              <Image width="221" height="47" src="/images/logo.png" />
            </Link> */}
            <div/>
            <Navbar navLinks={navLinks}/>
          </Container>
        </Toolbar>
      </AppBar>
      <Offset />
    </>
  );
};

export default Header;
