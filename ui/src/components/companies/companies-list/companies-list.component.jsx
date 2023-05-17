import { Fragment, useState } from "react";


const CompaniesList = () => {

    const [companies, setCompanies] = useState([]);

    const fetchCompaniesHandler = () => {
        fetch('http://localhost:5071/api/Company')
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                setCompanies(data);
            });
    }

    return (
        <Fragment>
            <section>
                <button onClick={fetchCompaniesHandler}>Get Companies</button>
            </section>
            <table>
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>
                        <th>
                            Industry
                        </th>
                        <th>
                            Sector
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {companies?.map((company) => (
                        <tr key={company.id}>
                            <td>{company.name}</td>
                            <td>{company.sector}</td>
                            <td>{company.industry}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </Fragment>
    );
}

export default CompaniesList;