import { Fragment, useState, useEffect, useCallback } from "react";
import './companies-list.style.css';


const CompaniesList = () => {

    const [sectors, setSectors] = useState([]);
    const [selectSectors, setSelectSectors] = useState([]);
    const [industries, setIndustries] = useState([]);
    const [selectIndustries, setSelectIndustries] = useState([]);
    const [tags, setTags] = useState([]);
    const [appliedTags, setAppliedTags] = useState([]);
    const [companies, setCompanies] = useState([]);

    const [selectedSector, setSelectedSector] = useState('');
    const [selectedIndustry, setSelectedIndustry] = useState('');

    const [sortingColumn, setSortingColumn] = useState('');
    const [sortingDirection, setSortingDirection] = useState('');

    const [roeAlarm, setRoeAlarm] = useState(30);
    const [priceBookAlarm, setPriceBookAlarm] = useState(2);
    const [last52WeekHighPerc, setlast52WeekHighPerc] = useState(10);
    

    useEffect(() => {
        fetch('http://localhost:5071/api/Company/filter', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify(
                { 
                    SectorId: selectedSector === '' ? 0 : selectedSector, 
                    IndustryId: selectedIndustry === '' ? 0 : selectedIndustry,
                    TagIds: appliedTags.map(t => t.id)
                }),
        })
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            data.forEach(company => {
                company.last52WeekHighPerc = (company.last52WeekHigh - company.price)/company.last52WeekHigh;
            });
            setCompanies(data);
        });
    }, [selectedSector, selectedIndustry, appliedTags]);

    useEffect(() => {
        fetch('http://localhost:5071/api/Company/options')
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                setSectors(data.sectors);
                const filteredSectors = data.sectors.map(({ id, name }) => ({ id, name }));
                filteredSectors.splice(0, 0, { id: 0, name: "all sectors"});
                setSelectSectors(filteredSectors)
                setSelectedSector(filteredSectors[0].id);

                setIndustries(data.industries);
                const filteredIndustries = data.industries.map(({ id, name }) => ({ id, name }));
                filteredIndustries.splice(0, 0, { id: 0, name: "all industries"});
                setSelectIndustries(filteredIndustries);
                setSelectedIndustry(filteredIndustries[0].id);

                setTags(data.tags);
            });
    }, []);

    const industryChangeHandler = (value) => {
        setSelectedIndustry(value);
    }

    const sectorChangeHandler = (value) => {
        let filteredIndustries = [];
        let intVal = parseInt(value);

        if(intVal !== 0) {
            filteredIndustries = industries.filter(item => item.sectorId === intVal)
            .map(({ id, name }) => ({ id, name }));
        }
        else {
            filteredIndustries = industries.map(({ id, name }) => ({ id, name }));
        } 

        filteredIndustries.splice(0, 0, { id: 0, name: "all"});
        
        setSelectIndustries(filteredIndustries);   
        setSelectedSector(intVal);    
        setSelectedIndustry(filteredIndustries[0].id);     
    }

    const sortCompanies = (column) => {
        let lastSortingColumn = sortingColumn;
        setSortingDirection(lastSortingColumn !== column ? 'asc' : sortingDirection === 'desc' ? 'asc' : 'desc');
        setSortingColumn(column);
        setCompanies(companies.sort(compare));
    }

    const shorten = (value) => {
        if(value.length > 20) {
            return value.substring(0,20) + '...';
        }
        return value;
    }

    const compare = (a, b) => {

        if(sortingColumn !== 'sector' && sortingColumn !== 'industry') {
            if(sortingDirection === 'asc'){
                if(Reflect.get(a, sortingColumn) < Reflect.get(b, sortingColumn)) return -1;
                if(Reflect.get(a, sortingColumn) > Reflect.get(b, sortingColumn)) return 1;
            }
            else {
                if(Reflect.get(a, sortingColumn) < Reflect.get(b, sortingColumn)) return 1;
                if(Reflect.get(a, sortingColumn) > Reflect.get(b, sortingColumn)) return -1;
            }
        }
        else {
            switch(sortingColumn) {
                case 'sector':
                    if(sortingDirection === 'asc'){
                        if(a.sector.name < b.sector.name) return -1;
                        if(a.sector.name > b.sector.name) return 1;
                    }
                    else {
                        if(a.sector.name < b.sector.name) return 1;
                        if(a.sector.name > b.sector.name) return -1;
                    }
                    break;
                case 'industry':
                    if(sortingDirection === 'asc'){
                        if(a.sector.name < b.industry.name) return -1;
                        if(a.sector.name > b.industry.name) return 1;
                    }
                    else {
                        if(a.sector.name < b.industry.name) return 1;
                        if(a.sector.name > b.industry.name) return -1;
                    }
                    break;
            }
        }

        return 0;
    }

    const tagApply = (id) => {
        let tag = tags.find(t => t.id === parseInt(id));
        let index = tags.indexOf(tag);
        tags.splice(index, 1);
        const newTags = tags.map(obj => ({...obj}));
        setTags(newTags);
    }

    const tagApply2 = (id) => {
        let tag = tags.find(t => t.id === parseInt(id));
        let appliedTag = appliedTags.find(t => t.id === parseInt(id));
        let index = appliedTags.indexOf(appliedTag);
        if(index === -1) {
            tag.className = "enabled";
            appliedTags.push(tag);
        }
        else {
            tag.className = "";
            appliedTags.splice(index, 1);
        }

        setTags(tags.map(obj => ({...obj})));
        setAppliedTags(appliedTags.map(obj => ({...obj})));
    }

    return (
        <Fragment>
            <fieldset>
                <legend>Filters</legend>
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
                </section>
                <section>
                    <ul className="availableTags">
                        {tags?.map((value) => (
                            <li key={value.id}>
                                <button value={value.id} onClick={e => tagApply2(e.target.value)} className={value.className}>{value.name}</button>
                            </li>
                        ))}
                    </ul>
                    <ul className="appliedTags"></ul>
                </section>
            </fieldset>
            <fieldset>
                <legend>Alarms</legend>
                <div>
                    <span>ROE % </span><input type="text" value={roeAlarm} onChange={e => setRoeAlarm(e.target.value)} />
                </div>
                <div>
                    <span>P/B </span><input type="text" value={priceBookAlarm} onChange={e => setPriceBookAlarm(e.target.value)} />
                </div>
                <div>
                    <span>L52W % </span><input type="text" value={last52WeekHighPerc} onChange={e => setlast52WeekHighPerc(e.target.value)} />
                </div>
            </fieldset>
            <table>
                <thead>
                    <tr>
                        <th>
                            <span onClick={e => sortCompanies('symbol')}>Symbol</span>
                        </th>
                        <th>
                            <span onClick={e => sortCompanies('name')}>Name</span>
                        </th>
                        <th>
                            <span onClick={e => sortCompanies('sector')}>Sector</span>
                        </th>
                        <th>
                            <span onClick={e => sortCompanies('industry')}>Industry</span>
                        </th>
                        <th>
                            <span onClick={e => sortCompanies('country')}>Country</span>
                        </th>
                        <th>
                            <span onClick={e => sortCompanies('price')}>Price</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('last52WeekHigh')}>52WH</span>
                        </th>
                        <th>
                            <span>%52WH</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('last52WeekLow')}>52WL</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('marketCap')}>MCap</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('ltM_PE')}>LTM PE</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('ltM_PE')}>NTM PE</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('priceBookRatio')}>P/B</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('dividendYield')}>Div %</span>
                        </th>
                        <th>
                        <span onClick={e => sortCompanies('roe')}>ROE %</span>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {companies?.map((company) => (
                        <tr key={company.id}>
                            <td>{company.symbol}</td>
                            <td><a href={company.website} target="blank">{shorten(company.name)}</a></td>
                            <td>{company.sector.name}</td>
                            <td>{company.industry.name}</td>
                            <td>{company.country}</td>
                            <td>{company.price}</td>
                            <td>{company.last52WeekHigh.toFixed(2)}</td>
                            <td className={company.last52WeekHighPerc * 100 <= last52WeekHighPerc ? 'green' : ''}>{(company.last52WeekHighPerc * 100).toFixed(2)}</td>
                            <td>{company.last52WeekLow.toFixed(2)}</td>
                            <td>{company.marketCap}</td>
                            <td>{company.ltM_PE.toFixed(2)}</td>
                            <td>{company.ltM_PE.toFixed(2)}</td>
                            <td className={company.priceBookRatio <= priceBookAlarm ? 'green' : ''}>{company.priceBookRatio.toFixed(2)}</td>
                            <td>{(company.dividendYield * 100).toFixed(2)}</td>
                            <td className={company.roe * 100 >= roeAlarm ? 'green' : ''}>{(company.roe * 100).toFixed(2)}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </Fragment>
    );
}

export default CompaniesList;