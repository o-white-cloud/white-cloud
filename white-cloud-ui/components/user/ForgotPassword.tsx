import { useCallback } from 'react';
import { Controller, useForm } from 'react-hook-form';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';

export interface ForgotPasswordProps {
  onSubmit: (email: string) => void;
}

interface ForgotPasswordFormData {
  email: string;
}

export const ForgotPassword: React.FC<ForgotPasswordProps> = (props) => {
  const { onSubmit } = props;
  const onFormSubmit = useCallback(
    (data: ForgotPasswordFormData) =>
    onSubmit(data.email),
    [onSubmit]
  );
  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<ForgotPasswordFormData>({
    defaultValues: {
      email: '',
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
          Introduceti email-ul dumneavoastra
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
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Recupereaza parola
          </Button>
        </Box>
      </Box>
    </>
  );
};
