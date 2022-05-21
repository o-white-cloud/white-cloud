import { useCallback } from 'react';
import { Controller, useForm } from 'react-hook-form';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';

export interface ResetPasswordProps {
  onSubmit: (password: string, confirmPassword: string) => void;
}

interface ResetPasswordFormData {
  password: string;
  confirmPassword: string;
}

export const ResetPassword: React.FC<ResetPasswordProps> = (props) => {
  const { onSubmit } = props;
  const onFormSubmit = useCallback(
    (data: ResetPasswordFormData) =>
      onSubmit(data.password, data.confirmPassword),
    [onSubmit]
  );
  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<ResetPasswordFormData>({
    defaultValues: {
      password: '',
      confirmPassword: '',
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
          Reseteaza parola
        </Typography>
        <Box
          component="form"
          onSubmit={handleSubmit(onFormSubmit)}
          noValidate
          sx={{ mt: 1 }}
        >
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
                label="Parola"
                type="password"
                id="password"
                error={errors.password !== undefined}
                helperText={errors.password?.message}
                autoComplete="new-password"
              />
            )}
          />

          <Controller
            name="confirmPassword"
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
                label="Confirma parola"
                type="password"
                id="password"
                error={errors.confirmPassword !== undefined}
                helperText={errors.confirmPassword?.message}
                autoComplete="confirm-new-password"
              />
            )}
          />

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Submit
          </Button>
        </Box>
      </Box>
    </>
  );
};
