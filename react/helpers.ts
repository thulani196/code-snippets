import React from 'react';
import axios, {Method} from "axios";


/**
 * @param {string} url API endpoint to fetch from
 * @param {string} method API method e.g [GET,POST,PUT...]
 * @param {object} data request body for the API method
 * @returns {object} response Response body for each request
 */
interface ApiResponse<T> {
  data?: T;
  error?: string;
  loading: boolean;
}
export function useFetch<FetchedData>(
  restUrl: string,
  restMethod: string,
  restData = {}
): ApiResponse<FetchedData> {
  const [response, setResponse] = React.useState<ApiResponse<FetchedData>>({
    loading: true
  });

  React.useEffect(() => {
    const fetchData = async () => {
      try {
        const { data } = await axios.request<FetchedData>({url: restUrl, method: restMethod as Method, data: restData || ''});
        
        setResponse({ data, loading: false, error: undefined });
      } catch (e) {
        setResponse({ data: undefined, loading: false, error: e });
      }
    };
    fetchData();
  }, [restUrl]);
  return response;
}
