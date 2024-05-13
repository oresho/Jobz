import React, { useEffect, useState } from 'react'
import JobListings from '../components/JobListings'

function JobsPage() {
    const [pageNo, setPageNo] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    function goToPreviousPage(){
        var prevPage = pageNo > 1 ? (pageNo - 1) : pageNo; 
        console.log(prevPage)
        setPageNo(prevPage);
    }

    function goToNextPage(){
        var nextPage = pageNo + 1; 
        console.log(nextPage)
        setPageNo(nextPage);
    }

    const hidePrev = (pageNo === 1) ? 'hidden' : '';
    const hideNext = (pageNo === totalPages) ? 'hidden' : '';

  return (
    <section className='bg-blue-50 px-4 py-6'>
        <JobListings pageNo={pageNo} setPageNo={setPageNo} setTotalPages={setTotalPages} />
        <div className='flex justify-center'>
            <button onClick={goToPreviousPage} disabled={pageNo === 1} className={`text-indigo-500 mx-5 mb-5 hover:text-indigo-600 ${hidePrev}`}>Previous</button>
            <button onClick={goToNextPage} disabled={pageNo === totalPages} className={`text-indigo-500 mx-5 mb-5 hover:text-indigo-600 ${hideNext}`}>Next</button>
        </div>
    </section>
  )
}

export default JobsPage