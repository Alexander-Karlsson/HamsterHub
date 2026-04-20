import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import type { HamsterDto } from '../types/types';
import type { CreateBookingRequest } from '../types/types';
import { getHamsterById, createBooking } from '../api/hamsterApi';
import { hamsterImages } from '../assets/hamsters';
import './BookingPage.css';

function BookingPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [hamster, setHamster] = useState<HamsterDto | null>(null);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);

  const [form, setForm] = useState({
    customerName: '',
    customerEmail: '',
    customerAddress: '',
    startDate: '',
    endDate: '',
    purpose: '',
  });

  useEffect(() => {
    if (id) getHamsterById(Number(id)).then(setHamster);
  }, [id]);

  const days = form.startDate && form.endDate
    ? Math.max(0, (new Date(form.endDate).getTime() - new Date(form.startDate).getTime()) / 86400000)
    : 0;

  const totalPrice = hamster ? days * hamster.pricePerDay : 0;

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (days <= 0) { setError('Slutdatum måste vara efter startdatum.'); return; }

    const request: CreateBookingRequest = {
      ...form,
      hamsterId: Number(id),
      startDate: new Date(form.startDate).toISOString(),
      endDate: new Date(form.endDate).toISOString(),
    };

    try {
      await createBooking(request);
      setSuccess(true);
    } catch {
      setError(`${hamster.name} är tyvärr inte tillgänglig under den valda perioden.`);
    }
  };

  if (success) return (
    <div className="booking-success">
      <h2>Bokning bekräftad!</h2>
      <p>{hamster?.name} är bokad i {days} dagar för {totalPrice} kr.</p>
      <p>Kvittot skiter vi i...</p>
      <button onClick={() => navigate('/')}>Tillbaka till startsidan</button>
    </div>
  );

  return (
    <div className="booking-page">
      <button className="back-btn" onClick={() => navigate('/')}>← Tillbaka</button>

      {hamster && (
        <div className="booking-layout">

          
          <div className="hamster-summary">
            <div className="summary-img">
              {hamsterImages[hamster.id]
                ? <img src={hamsterImages[hamster.id]} alt={hamster.name} />
                : <span>{hamster.name[0]}</span>
              }
            </div>
            <h2>{hamster.name}</h2>
            <span className="personality">{hamster.personality}</span>
            <p className="description">{hamster.description}</p>
            <div className="price-tag">
              {hamster.pricePerDay} kr<small>/dag</small>
            </div>
          </div>

        
          <form className="booking-form" onSubmit={handleSubmit}>
            <h3>Din bokning</h3>

            <div className="field">
              <label>För- och efternamn</label>
              <input name="customerName" value={form.customerName} onChange={handleChange} required />
            </div>

            <div className="field">
              <label>E-postadress</label>
              <input name="customerEmail" type="email" value={form.customerEmail} onChange={handleChange} required />
            </div>

            <div className="field">
              <label>Avdelning / adress</label>
              <input name="customerAddress" value={form.customerAddress} onChange={handleChange} required />
            </div>

            <div className="field-row">
              <div className="field">
                <label>Startdatum</label>
                <input name="startDate" type="date" value={form.startDate} onChange={handleChange} required />
              </div>
              <div className="field">
                <label>Slutdatum</label>
                <input name="endDate" type="date" value={form.endDate} onChange={handleChange} required />
              </div>
            </div>

            <div className="field">
              <label>Syfte</label>
              <textarea name="purpose" value={form.purpose} onChange={handleChange} rows={3} />
            </div>

            {days > 0 && (
              <div className="price-summary">
                <span>{days} dagar × {hamster.pricePerDay} kr</span>
                <strong>{totalPrice} kr</strong>
              </div>
            )}

            {error && <p className="form-error">{error}</p>}

            <button type="submit" className="submit-btn">Bekräfta bokning</button>
          </form>
        </div>
      )}
    </div>
  );
}

export default BookingPage;