import React from 'react';
import { Control, Controller, ControllerProps } from 'react-hook-form';

import FormControl, { FormControlProps } from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormHelperText from '@mui/material/FormHelperText';
import FormLabel from '@mui/material/FormLabel';
import Radio from '@mui/material/Radio';
import RadioGroup, { RadioGroupProps } from '@mui/material/RadioGroup';

export interface FormRadioGroupProps
  extends FormControlProps,
    Pick<ControllerProps, 'rules'> {
  name: string;
  defaultValue?: any;
  RadioGroupProps?: RadioGroupProps;
  label?: string;
  helperText?: string;
  radios?: { label: string; value: any }[];
  control: Control<any, object>;
}

export const FormRadioGroup: React.FC<FormRadioGroupProps> = (props) => {
  const {
    name,
    defaultValue,
    label,
    helperText,
    radios,
    control,
    rules,
    RadioGroupProps,
    ...formControlProps
  } = props;
  if (!name) {
    return <p>No field name provided</p>;
  }

  return (
    <Controller
      name={name}
      control={control}
      rules={rules}
      render={({ field: { onChange, value }, fieldState }) => (
        <FormControl {...formControlProps} error={!!fieldState.error}>
          {label && (
            <FormLabel id={`${name}--radio-buttons-group-label`}>
              {label}
            </FormLabel>
          )}
          <RadioGroup
            {...RadioGroupProps}
            value={value ?? 0}
            onChange={onChange}
          >
            {radios
              ? radios.map((r) => (
                  <FormControlLabel
                    key={r.value}
                    value={r.value}
                    control={<Radio />}
                    label={r.label}
                  />
                ))
              : props.children}
          </RadioGroup>
          <FormHelperText>
            {fieldState.error ? fieldState.error : helperText}
          </FormHelperText>
        </FormControl>
      )}
    />
  );
};
