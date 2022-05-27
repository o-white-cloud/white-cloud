import { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { Controller, SubmitHandler, useForm } from 'react-hook-form';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import FormControlLabel from '@mui/material/FormControlLabel';
import Grid from '@mui/material/Grid';
import Link from '@mui/material/Link';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';

export interface RegisterFormData {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  copsiNumber?: string;
  accountType: AccountType;
}

export enum AccountType {
  Personal = 1,
  Therapist = 2,
}

export interface RegisterProps {
  onRegister: (data: RegisterFormData) => {};
  email?: string;
  inviteById?: string;
  signInUrl: string;
}
export const Register: React.FC<RegisterProps> = (props) => {
  const { onRegister, signInUrl, email, inviteById } = props;
  const [accountType, setAccountType] = useState<AccountType>(
    AccountType.Personal
  );
  const {
    control,
    reset,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<RegisterFormData>({
    defaultValues: {
      firstName: '',
      lastName: '',
      email: email ? email : '',
      password: '',
      accountType: AccountType.Personal,
    },
  });

  useEffect(() => {
    if(email) {
      reset({
        firstName: '',
        lastName: '',
        email: email,
        password: '',
        accountType: AccountType.Personal,
      })
    }
  }, [email, reset]);

  const onFormSubmit = useCallback<SubmitHandler<RegisterFormData>>(
    (data) => {
      onRegister({ ...data, accountType: accountType });
    },
    [onRegister, accountType]
  );

  const onAccountTypeChange = useCallback(
    (event: ChangeEvent<HTMLInputElement>, value: string) => {
      setAccountType(Number(value) as AccountType);
    },
    [setAccountType]
  );

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      }}
    >
      <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
        <LockOutlinedIcon />
      </Avatar>
      <Typography component="h1" variant="h5">
        Sign up
      </Typography>
      <form onSubmit={handleSubmit(onFormSubmit)}>
        <Box sx={{ mt: 3 }}>
          <Grid container spacing={2}>
            {!email && (
              <Grid item xs={12} direction="row">
                <RadioGroup
                  aria-labelledby="account-type"
                  defaultValue={AccountType.Personal}
                  onChange={onAccountTypeChange}
                  name="account-type-radio-buttons-group"
                  sx={{ flexDirection: 'row' }}
                >
                  <Grid item xs={6}>
                    <FormControlLabel
                      value={AccountType.Personal}
                      control={<Radio />}
                      label="Personal"
                    />
                  </Grid>
                  <Grid item xs={6}>
                    <FormControlLabel
                      value={AccountType.Therapist}
                      control={<Radio />}
                      label="Psiholog"
                    />
                  </Grid>
                </RadioGroup>
              </Grid>
            )}
            <Grid item xs={12} sm={6}>
              <Controller
                name="firstName"
                control={control}
                render={({ field }) => (
                  <TextField
                    autoComplete="given-name"
                    {...field}
                    required
                    fullWidth
                    id="firstName"
                    label="First Name"
                    autoFocus
                  />
                )}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <Controller
                name="lastName"
                control={control}
                render={({ field }) => (
                  <TextField
                    required
                    {...field}
                    fullWidth
                    id="lastName"
                    label="Last Name"
                    autoComplete="family-name"
                  />
                )}
              />
            </Grid>
            <Grid item xs={12}>
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
                    required
                    {...field}
                    fullWidth
                    disabled={email !== undefined}
                    InputProps={{ readOnly: email !== undefined }}
                    id="email"
                    label="Email"
                    autoComplete="email"
                  />
                )}
              />
            </Grid>
            <Grid item xs={12}>
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
                    required
                    {...field}
                    fullWidth
                    label="Parola"
                    type="password"
                    id="password"
                    autoComplete="new-password"
                  />
                )}
              />
            </Grid>
            {accountType === AccountType.Therapist && (
              <Grid item xs={12}>
                <Controller
                  name="copsiNumber"
                  control={control}
                  rules={{
                    required: {
                      value: true,
                      message: 'Numarul COPSI este obligatoriu',
                    },
                  }}
                  render={({ field }) => (
                    <TextField
                      required
                      {...field}
                      fullWidth
                      label="Numar COPSI"
                      id="copsiNumber"
                      autoComplete="new-copsi-number"
                    />
                  )}
                />
              </Grid>
            )}
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign Up
          </Button>
          <Grid container justifyContent="flex-end">
            <Grid item>
              <Link href={signInUrl} variant="body2">
                Ai deja un cont? Sign in
              </Link>
            </Grid>
          </Grid>
        </Box>
      </form>
    </Box>
  );
};
