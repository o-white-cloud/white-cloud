import React from 'react';
import { Control, Controller, ControllerProps } from 'react-hook-form';

import { OutlinedTextFieldProps, TextField } from '@mui/material';

export interface FormTextFieldProps
  extends Omit<OutlinedTextFieldProps, 'variant'>,
    Pick<ControllerProps, 'rules'> {
  control: Control<any, object>;
}

export const FormTextField: React.FC<FormTextFieldProps> = (props) => {
  const { name, control, label, rules, ...otherProps } = props;
  if (!name) {
    return <p>No field name provided</p>;
  }

  return (
    <Controller
      name={name}
      control={control}
      rules={rules}
      render={({ field:{value, onChange, onBlur}, fieldState }) => (
        <TextField
          {...otherProps}
          value={value ?? ''}
          onChange={onChange}
          onBlur={onBlur}
          label={label}
          variant="outlined"
          autoComplete={`new-${name}`}
          error={fieldState.error !== undefined}
          helperText={fieldState.error ? fieldState.error.message : undefined}
        />
      )}
    />
  );
};
