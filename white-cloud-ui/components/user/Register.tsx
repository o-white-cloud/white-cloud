import { FormRadioGroup, FormTextField } from 'components/forms';
import { Gender } from 'models';
import { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { SubmitHandler, useForm } from 'react-hook-form';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import LoadingButton from '@mui/lab/LoadingButton';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import FormControlLabel from '@mui/material/FormControlLabel';
import Grid from '@mui/material/Grid';
import Link from '@mui/material/Link';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import Typography from '@mui/material/Typography';

export interface RegisterFormData {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  copsiNumber?: string;
  accountType: AccountType;
  age?: number;
  gender?: number;
  ocupation?: string;
}

export enum AccountType {
  Personal = 1,
  Therapist = 2,
}

export interface RegisterProps {
  onRegister: (data: RegisterFormData) => {};
  loading: boolean;
  email?: string;
  inviteMode?: boolean;
  signInUrl: string;
}
export const Register: React.FC<RegisterProps> = (props) => {
  const { onRegister, signInUrl, email, inviteMode, loading } = props;
  const [accountType, setAccountType] = useState<AccountType>(
    AccountType.Personal
  );
  const { control, reset, getValues, handleSubmit } = useForm<RegisterFormData>(
    {
      defaultValues: {
        firstName: '',
        lastName: '',
        email: email ? email : '',
        password: '',
        accountType: AccountType.Personal,
        age: undefined,
        gender: undefined,
        ocupation: undefined,
      },
    }
  );

  const formValues = getValues();
  useEffect(() => {
    console.log(JSON.stringify(formValues));
  }, [formValues]);

  useEffect(() => {
    if (email) {
      reset({
        firstName: '',
        lastName: '',
        email: email,
        password: '',
        accountType: AccountType.Personal,
        age: undefined,
        gender: undefined,
        ocupation: undefined,
      });
    }
  }, [email, reset]);

  const onFormSubmit = useCallback<SubmitHandler<RegisterFormData>>(
    (data) => {
      debugger;
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
            {!inviteMode && (
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
              <FormTextField
                name="firstName"
                control={control}
                label="Prenume"
                required
                fullWidth
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <FormTextField
                name="lastName"
                control={control}
                label="Nume"
                required
                fullWidth
              />
            </Grid>
            <Grid item xs={12}>
              <FormTextField
                name="email"
                control={control}
                label="Email"
                required
                fullWidth
                InputProps={{ readOnly: inviteMode }}
                rules={{
                  required: {
                    value: true,
                    message: 'Email cannot be empty',
                  },
                }}
              />
            </Grid>
            <Grid item xs={12}>
              <FormTextField
                name="password"
                control={control}
                label="Parola"
                required
                type="password"
                fullWidth
                rules={{
                  required: {
                    value: true,
                    message: 'Parola este obligatorie',
                  },
                }}
              />
            </Grid>
            {inviteMode && (
              <>
                <Grid item xs={12}>
                  <FormTextField
                    name="age"
                    control={control}
                    label="Varsta"
                    required
                    type="number"
                    rules={{
                      required: {
                        value: true,
                        message: 'Varsta trebuie sa fie intre 14 si 100 ani',
                      },
                      min: 14,
                      max: 110,
                    }}
                  />
                </Grid>
                <Grid item xs={12}>
                  <FormRadioGroup
                    radios={[
                      { label: 'Feminin', value: Gender.Female },
                      { label: 'Masculin', value: Gender.Male },
                      { label: 'Altceva', value: Gender.Other },
                    ]}
                    name="gender"
                    label="Sex"
                    defaultValue={Gender.Other}
                    control={control}
                  />
                </Grid>{' '}
                <Grid item xs={12}>
                  <FormTextField
                    name="ocupation"
                    control={control}
                    label="Ocupatia"
                    required
                    fullWidth
                    rules={{
                      required: {
                        value: true,
                        message: 'Campul este obligatoriu',
                      },
                    }}
                  />
                </Grid>
              </>
            )}
            {accountType === AccountType.Therapist && (
              <Grid item xs={12}>
                <FormTextField
                  name="copsiNumber"
                  control={control}
                  label="Cod parafa Colegiul Psihologilor"
                  required
                />
              </Grid>
            )}
          </Grid>
          <LoadingButton
            type="submit"
            fullWidth
            loading={loading}
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign Up
          </LoadingButton>
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
