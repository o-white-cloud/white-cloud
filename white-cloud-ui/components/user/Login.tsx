import Link from 'components/Link';
import { useRouter } from 'next/router';
import { useCallback, useState } from 'react';
import { Controller, useForm } from 'react-hook-form';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import LoadingButton from '@mui/lab/LoadingButton';
import Alert from '@mui/material/Alert';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';

export interface LoginProps {
  onLogin: (email: string, password: string, rememberMe: boolean) => void;
  forgotPasswordUrl: string;
  registerUrl: string;
  error: string | undefined;
  loading: boolean;
}

interface LoginFormData {
  email: string;
  password: string;
  rememberMe: boolean;
}

export const useLogin = (
  loginUrl: string,
  redirectUrl: string
): [
  (email: string, password: string, rememberMe: boolean) => void,
  string | undefined,
  boolean
] => {
  const [loginError, setLoginError] = useState<string>();
  const [loading, setLoading] = useState(false);
  const router = useRouter();

  const onLogin = useCallback(
    async (email: string, password: string, rememberMe: boolean) => {
      setLoading(true);
      try {
        const response = await fetch(loginUrl, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            email,
            password,
            rememberMe,
          }),
        });
        if (response.ok) {
          router.push(redirectUrl);
          return;
        }

        if (response.status === 422) {
          setLoginError('Adresa de email nu a fost confirmata!');
          return;
        }

        if (response.status === 401) {
          setLoginError('Utilizator sau parola incorecta!');
          return;
        }
      } catch (e) {
        console.log(e);
      } finally {
        setLoading(false);
      }
    },
    [router, loginUrl, redirectUrl]
  );
  return [onLogin, loginError, loading];
};

export const Login: React.FC<LoginProps> = (props) => {
  const { onLogin, error, forgotPasswordUrl, registerUrl, loading} = props;
  const onFormSubmit = useCallback(
    (data: LoginFormData) =>
      onLogin(data.email, data.password, data.rememberMe),
    [onLogin]
  );
  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormData>({
    defaultValues: {
      email: '',
      password: '',
    },
  });

  return (
    <>
      <Box
        sx={{
          marginTop: 8,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <Box
          component="form"
          onSubmit={handleSubmit(onFormSubmit)}
          noValidate
          sx={{ mt: 1 }}
        >
          <Controller
            name="email"
            control={control}
            rules={{
              required: {
                value: true,
                message: 'Email cannot be empty',
              },
            }}
            render={({ field }) => (
              <TextField
                {...field}
                margin="normal"
                required
                fullWidth
                label="Email Address"
                autoComplete="email"
                autoFocus
              />
            )}
          />

          <Controller
            name="password"
            control={control}
            rules={{
              required: {
                value: true,
                message: 'Password cannot be empty',
              },
            }}
            render={({ field }) => (
              <TextField
                {...field}
                margin="normal"
                required
                fullWidth
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
              />
            )}
          />

          {error && <Alert severity="error">{error}</Alert>}
          <FormControlLabel
            control={<Checkbox value="remember" color="primary" />}
            label="Remember me"
          />
          <LoadingButton
            type="submit"
            fullWidth
            variant="contained"
            loading={loading}
            sx={{ mt: 3, mb: 2 }}
          >
            Sign In
          </LoadingButton>
          <Grid container>
            <Grid item xs>
              <Link href={forgotPasswordUrl} variant="body2">
                Forgot password?
              </Link>
            </Grid>
            <Grid item>
              <Link href={registerUrl} variant="body2">
                {"Don't have an account? Sign Up"}
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </>
  );
};
