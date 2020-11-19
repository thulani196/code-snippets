import React, { createContext, useState } from 'react';
import { AUTH_TOKEN_KEY } from '../../../utils/constants';
import { useFetch } from '../../../utils/helpers';

interface Props {
  children: React.ReactNode;
}

const ContextProvider = createContext({});
export const Context = ContextProvider;

export default function AuthProvider({ children }: Props) {
  const { data } = useFetch('/me', 'GET');

  return (
    <ContextProvider.Provider value={data ? data : {}}>
      {children}
    </ContextProvider.Provider>
  );
}
