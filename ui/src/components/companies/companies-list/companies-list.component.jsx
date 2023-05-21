import { Fragment, useState, useEffect, useCallback } from "react";


const CompaniesList = () => {

    const [sectors, setSectors] = useState([]);
    const [selectSectors, setSelectSectors] = useState([]);
    const [industries, setIndustries] = useState([]);
    const [selectIndustries, setSelectIndustries] = useState([]);
    const [tags, setTags] = useState([]);
    const [companies, setCompanies] = useState([]);

    const [selectedSector, setSelectedSector] = useState('');
    const [selectedIndustry, setSelectedIndustry] = useState('');

    useEffect(() => {
        fetch('http://localhost:5071/api/Company/filter', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify({ SectorId: selectedSector === '' ? 0 : selectedSector, IndustryId: selectedIndustry === '' ? 0 : selectedIndustry}),
        })
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            setCompanies(data);
        });
    }, [selectedSector, selectedIndustry]);

    useEffect(() => {
        fetch('http://localhost:5071/api/Company/options')
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                setSectors(data.sectors);
                const adaptedSectors = data.sectors.map(({ id, name }) => ({ id, name }));
                adaptedSectors.splice(0, 0, { id: 0, name: "all"});
                setSelectSectors(adaptedSectors)
                setSelectedSector(adaptedSectors[0].id);

                setIndustries(data.industries);
                const adaptedIndustries = data.industries.map(({ id, name }) => ({ id, name }));
                adaptedIndustries.splice(0, 0, { id: 0, name: "all"});
                setSelectIndustries(adaptedIndustries);
                setSelectedIndustry(adaptedIndustries[0].id);

                setTags(data.tags);
            });
    }, []);

    const industryChangeHandler = (value) => {
        setSelectedIndustry(value);
    }

    const sectorChangeHandler = (value) => {
        let adaptedIndustries = [];
        console.log(industries);
        if(value !== 0) {
            adaptedIndustries = industries.filter(function(item) {
                return item.sectorId === value
            })
            .map(({ id, name }) => ({ id, name }));
        }
        else {
            adaptedIndustries = industries.map(({ id, name }) => ({ id, name }));
        }
          
        console.log(adaptedIndustries);

        adaptedIndustries.splice(0, 0, { id: 0, name: "all"});
        
        setSelectIndustries(adaptedIndustries);       
        setSelectedIndustry(adaptedIndustries[0].id);

        setSelectedSector(value);
    }

    return (
        <Fragment>
            <section>
                <select value={selectedSector || ''} onChange={e => sectorChangeHandler(e.target.value)}>
                    {selectSectors?.map((value) => (
                        <option value={value.id} key={value.id}>{value.name}</option>
                    ))}
                </select>
                <select value={selectedIndustry || ''} onChange={e => industryChangeHandler(e.target.value)}>
                    {selectIndustries?.map((value) => (
                        <option value={value.id} key={value.id}>{value.name}</option>
                    ))}
                </select>
                <select>
                    {tags?.map((value) => (
                        <option value={value.id} key={value.id}>{value.name}</option>
                    ))}
                </select>
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
                            <td>{company.sector.name}</td>
                            <td>{company.industry.name}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </Fragment>
    );
}

export default CompaniesList;