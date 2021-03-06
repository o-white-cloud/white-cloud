import '../styles/globals.css';

import Header from 'components/Header';
import Head from 'next/head';

import { CacheProvider, EmotionCache } from '@emotion/react';
import { GlobalStyles } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';
import { ThemeProvider } from '@mui/material/styles';

import createEmotionCache from '../styles/createEmotionCache';
import theme from '../styles/theme';

import type { AppProps } from 'next/app'
const clientSideEmotionCache = createEmotionCache();

interface MyAppProps extends AppProps{
  emotionCache: EmotionCache
}

function MyApp(props: MyAppProps) {
  const { Component, emotionCache = clientSideEmotionCache, pageProps } = props;

  return (
    <CacheProvider value={emotionCache}>
      <Head>
        <title>White Cloud</title>
        <meta name="viewport" content="initial-scale=1, width=device-width" />
      </Head>
      <ThemeProvider theme={theme}>
        {/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
        <CssBaseline />
        <GlobalStyles
          styles={{
            body: { backgroundColor: '#e7ebf0' },
          }}
        />
        <Header/>
        <Component {...pageProps} />
      </ThemeProvider>
    </CacheProvider>
  );
}

export default MyApp
