import Image from 'next/image';
import React from 'react';

import ArrowDownward from '@mui/icons-material/ArrowDownward';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';

interface HeroProps {
  imgSrc: string;
  imgAlt: string;
  title: string;
  subtitle: string;
}
const Hero: React.FC<HeroProps> = (props) => {
  const { imgSrc, imgAlt, title, subtitle } = props;

  return (
    <Grid
      component="section"
      container
      sx={{
        position: `relative`,
        height: '100vh',
        width: `100vw`,
        overflow: `hidden`,
        maxWidth: `100%`,
        zIndex: -100,
        mb: 15,
      }}
    >
      <Image src={imgSrc} alt={imgAlt} layout="fill" objectFit="cover" />
      <Grid
        container
        sx={{
          position: 'absolute',
          inset: 0,
          backgroundColor: 'rgba(0,0,0, .7)',
        }}
      ></Grid>
      <Grid
        container
        item
        flexDirection="column"
        justifyContent="center"
        alignItems="center"
        sx={{ zIndex: 1, bottom: 100, position: 'absolute'}}
      >
        <Typography
          variant="h1"
          align="center"
          gutterBottom
          sx={{
            color: 'secondary.main',
            fontWeight: 400,
          }}
        >
          {title}
        </Typography>
        <Typography
          component="p"
          variant="h3"
          align="center"
          color="common.white"
          sx={{
            mb: 10,
          }}
        >
          {subtitle}
        </Typography>
        <Typography component="p" variant="h6" color="secondary" gutterBottom>
          Scroll
        </Typography>
        <ArrowDownward fontSize="large" color="secondary" />
      </Grid>
    </Grid>
  );
};

export default Hero;
