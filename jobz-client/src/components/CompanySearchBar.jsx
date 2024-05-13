import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { FaSearch } from 'react-icons/fa';

function CompanySearchBar({ setCompanyId }) {
  const [company, setCompany] = useState('');
  const [companyList, setCompanyList] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    // Define a variable to hold the timeout ID
    let timeoutId;

    // Function to fetch companies when the input value changes
    const fetchCompanies = async () => {
      if (company.trim() !== '') {
        setLoading(true);
        try {
          const response = await axios.get(`https://localhost:7058/api/v1/companies?name=${company}`);
          setCompanyList(response.data.data);
        } catch (error) {
          console.error('Error fetching companies:', error);
        } finally {
          setLoading(false);
        }
      } else {
        setCompanyList([]);
      }
    };

    // Debounce the fetchCompanies function
    const debounceFetchCompanies = () => {
      clearTimeout(timeoutId);
      timeoutId = setTimeout(fetchCompanies, 500); // Adjust the delay as needed (500 milliseconds in this example)
    };

    // Call the debounceFetchCompanies function when the input value changes
    debounceFetchCompanies();

    // Cleanup function to clear the timeout on component unmount
    return () => {
      clearTimeout(timeoutId);
    };
  }, [company]);

  return (
    <div className="mb-4">
      <label htmlFor="company" className="block text-gray-700 font-bold mb-2">
      <FaSearch className="mr-2 inline-block" />
        Company Name
      </label>
      <input
        type="text"
        id="company"
        name="company"
        className="border rounded w-full py-2 px-3"
        placeholder="Search for a company..."
        value={company}
        onChange={(e) => setCompany(e.target.value)}
      />
      {loading ? (
        <p>Loading...</p>
      ) : (
        <ul className="mt-1 rounded border bg-white absolute">
          {companyList.map((company) => (
            <li
              key={company.id}
              className="py-2 px-3 cursor-pointer hover:bg-gray-200"
              onClick={() => {
                setCompany(company.name);
                setCompanyId(company.id);
                setCompanyList([]);
              }}
            >
              {company.name}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default CompanySearchBar;
