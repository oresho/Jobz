import React, { useState } from 'react'

function AddCompanyPage() {
    const [name, setName] = useState('');
    const [description, setDecsription] = useState('');
    const [contactEmail, setContactEmail] = useState('');
    const [contactPhone, setContactPhone] = useState('');

    const submitForm = () => {
        const newCompany = {
            name,
            description,
            contactEmail,
            contactPhone
        }
    }

    return (
        <>
            <section className="bg-indigo-50">
          <div className="container m-auto max-w-2xl py-24">
            <div
              className="bg-white px-6 py-8 mb-4 shadow-md rounded-md border m-4 md:m-0"
            >
              <form onSubmit={submitForm}>
                <h2 className="text-3xl text-center font-semibold mb-6">Add Company</h2>
                <h3 className="text-2xl mb-5">Company Info</h3>
    
                <div className="mb-4">
                  <label htmlFor="company" className="block text-gray-700 font-bold mb-2"
                    >Company Name</label
                  >
                  <input
                    type="text"
                    id="company"
                    name="company"
                    className="border rounded w-full py-2 px-3"
                    placeholder="Company Name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                  />
                </div>
    
                <div className="mb-4">
                  <label
                    htmlFor="company_description"
                    className="block text-gray-700 font-bold mb-2"
                    >Company Description</label
                  >
                  <textarea
                    id="company_description"
                    name="company_description"
                    className="border rounded w-full py-2 px-3"
                    rows="4"
                    placeholder="What does your company do?"
                    value={description}
                    onChange={(e) => setDecsription(e.target.value)}
                  ></textarea>
                </div>
    
                <div className="mb-4">
                  <label
                    htmlFor="contact_email"
                    className="block text-gray-700 font-bold mb-2"
                    >Contact Email</label
                  >
                  <input
                    type="email"
                    id="contact_email"
                    name="contact_email"
                    className="border rounded w-full py-2 px-3"
                    placeholder="Email address for applicants"
                    required
                    value={contactEmail}
                    onChange={(e) => setContactEmail(e.target.value)}
                  />
                </div>
                <div className="mb-4">
                  <label
                    htmlFor="contact_phone"
                    className="block text-gray-700 font-bold mb-2"
                    >Contact Phone</label
                  >
                  <input
                    type="tel"
                    id="contact_phone"
                    name="contact_phone"
                    className="border rounded w-full py-2 px-3"
                    placeholder="Optional phone for applicants"
                    value={contactPhone}
                    onChange={(e) => setContactPhone(e.target.value)}
                  />
                </div>
    
                <div>
                  <button
                    className="bg-indigo-500 hover:bg-indigo-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
                    type="submit"
                  >
                    Add Company
                  </button>
                </div>
              </form>
            </div>
          </div>
        </section>
        </>
      )
}

export default AddCompanyPage