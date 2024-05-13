import React, { useEffect, useState } from 'react'
import axios from 'axios';
import JobListing from './JobListing'
import Spinner from './Spinner.jsx';

const client = axios.create({
    baseURL: "https://localhost:7058/api/v1/jobs" 
  });
  
function JobListings({isHome = false, pageNo, setTotalPages = () => {}}) {
    const [jobs, setJobs] = useState([]);
    const [loading, setLoading] = useState(true);

    const getJobs = async () => {
        let queryParam = isHome ? '' : `?pageNo=${pageNo}&pageSize=6`;

        try {
            const response = await client.get(queryParam);
            const paginatedResponse = response.data.data;
            setTotalPages(paginatedResponse.totalPages);
            setJobs(paginatedResponse.data);
        } catch (error) {
            console.log('Error Fetching Data', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        getJobs();
    }, [pageNo, isHome, setTotalPages]);

  return (
    <section className="bg-blue-50 px-4 py-10">
      <div className="container-xl lg:container m-auto">
        <h2 className="text-3xl font-bold text-indigo-500 mb-6 text-center">
          { isHome ? 'Recent Jobs' : 'Browse Jobs' }
        </h2>
        
            {
                loading ? (<Spinner loading={loading}/>) : 
                <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                    {(jobs.map((job) => 
                        <JobListing key={job.id} job={job}/>
                    ))}
                </div>
            }
          
        
      </div>
    </section>
  )
}

export default JobListings