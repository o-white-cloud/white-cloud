import Link from 'components/Link';
import { TestModel } from 'models';
import React from 'react';

import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { styled } from '@mui/system';
import Box from '@mui/system/Box';

import TestIcon from './icons/Test';
import TestMultipleIntelligenceIcon from './icons/TestMultipleIntelligence';
import TestProffesionalIcon from './icons/TestProffesional';

interface TestCardProps {
  test: TestModel;
}

const testIcons: { [key: string]: React.ReactElement } = {
  test: <TestIcon width={200} height={200} />,
  multipleInt: <TestMultipleIntelligenceIcon width={200} height={200} />,
  proff: <TestProffesionalIcon width={200} height={200} />,
};

const TestCard: React.FC<TestCardProps> = (props) => {
  return (
    <Box sx={{ width: 275 }}>
      <Link href={`/tests/${props.test.id}`} noLinkStyle>
        <Card>
          <React.Fragment>
            <CardContent>
              {testIcons[props.test.icon]}
              <Typography variant="h5" component="div">
                {props.test.name}
              </Typography>
              <Typography sx={{ mb: 1.5 }} color="text.secondary">
                {props.test.excerpt}
              </Typography>
            </CardContent>
          </React.Fragment>
        </Card>
      </Link>
    </Box>
  );
};

export default TestCard;
