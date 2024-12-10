using API.Model.DTO;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;


[Route("api/[controller]")]
[ApiController]
public class LaboratoryRequestsController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public LaboratoryRequestsController(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/LaboratoryRequests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LaboratoryRequestDto>>> GetLaboratoryRequests()
    {
        var labRequests = await _context.LaboratoryRequests
            .Include(l => l.Parasitology)
            .Include(l => l.Hematology)
            .Include(l => l.Biochemistry)
            .Include(l => l.Bacteriology)
            .Include(l => l.Serology)
            .Include(l => l.CardiacMarker)
            .Include(l => l.CancerMarker)
            .Include(l => l.Electrolyte)
            .Include(l => l.OtherLab)
            .Include(l => l.Hormone)
            .Include(l => l.Coagulation)
            .ToListAsync();

        return _mapper.Map<List<LaboratoryRequestDto>>(labRequests);
    }

    // GET: api/LaboratoryRequests/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LaboratoryRequestDto>> GetLaboratoryRequest(int id)
    {
        var labRequest = await _context.LaboratoryRequests
            .Include(l => l.Parasitology)
            .Include(l => l.Hematology)
            .Include(l => l.Biochemistry)
            .Include(l => l.Bacteriology)
            .Include(l => l.Serology)
            .Include(l => l.CardiacMarker)
            .Include(l => l.CancerMarker)
            .Include(l => l.Electrolyte)
            .Include(l => l.OtherLab)
            .Include(l => l.Hormone)
            .Include(l => l.Coagulation)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (labRequest == null)
        {
            return NotFound();
        }

        return _mapper.Map<LaboratoryRequestDto>(labRequest);
    }

    [HttpPost]
    public async Task<IActionResult> PostLaboratoryRequest([FromBody] LaboratoryRequestDto labRequestDto)
    {
        var patientExists = await _context.Patients.AnyAsync(p => p.Id == labRequestDto.PatientId);
        if (!patientExists)
        {
            return BadRequest("Invalid PatientId. The specified patient does not exist.");
        }

        var laboratoryRequest = new LaboratoryRequest
        {
            DateOfRequest = labRequestDto.DateOfRequest,
            PatientId = labRequestDto.PatientId,
            hospitalName=labRequestDto.hospitalName,
            IsUrinalysisChecked = labRequestDto.IsUrinalysisChecked,
            IsBacteriologyChecked = labRequestDto.IsBacteriologyChecked,
            IsBiochemistryChecked = labRequestDto.IsBiochemistryChecked,
            IsHematologyChecked = labRequestDto.IsHematologyChecked,
            IsParasitologyChecked = labRequestDto.IsParasitologyChecked,
            IsSerologyChecked = labRequestDto.IsSerologyChecked,
            IsCancerMarkerChecked = labRequestDto.IsCancerMarkerChecked,
            IsCardiacMarkerChecked = labRequestDto.IsCardiacMarkerChecked,
            IsCoagulationChecked = labRequestDto.IsCoagulationChecked,
            IsElectrolyteChecked = labRequestDto.IsElectrolyteChecked,
            IsHormoneChecked = labRequestDto.IsHormoneChecked,
            IsOtherLabChecked = labRequestDto.IsOtherLabChecked,
            ParasitologyId = labRequestDto.IsParasitologyChecked ? (await GetOrCreateParasitologyAsync(labRequestDto.Parasitology))?.Id : (int?)null,
            HematologyId = labRequestDto.IsHematologyChecked ? (await GetOrCreateHematologyAsync(labRequestDto.Hematology))?.Id : (int?)null,
            BiochemistryId = labRequestDto.IsBiochemistryChecked ? (await GetOrCreateBiochemistryAsync(labRequestDto.Biochemistry))?.Id : (int?)null,
            BacteriologyId = labRequestDto.IsBacteriologyChecked ? (await GetOrCreateBacteriologyAsync(labRequestDto.Bacteriology))?.Id : (int?)null,
            SerologyId = labRequestDto.IsSerologyChecked ? (await GetOrCreateSerologyAsync(labRequestDto.Serology))?.Id : (int?)null,
            CardiacMarkerid = labRequestDto.IsCardiacMarkerChecked ? (await GetOrCreateCardiacMarkerAsync(labRequestDto.CardiacMarker))?.id : (int?)null,
            CancerMarkerid = labRequestDto.IsCancerMarkerChecked ? (await GetOrCreateCancerMarkerAsync(labRequestDto.CancerMarker))?.id : (int?)null,
            Electrolyteid = labRequestDto.IsElectrolyteChecked ? (await GetOrCreateElectrolyteAsync(labRequestDto.Electrolyte))?.id : (int?)null,
            Hormoneid = labRequestDto.IsHormoneChecked ? (await GetOrCreateHormoneAsync(labRequestDto.Hormone))?.Id : (int?)null,
            Coagulationid = labRequestDto.IsCoagulationChecked ? (await GetOrCreateCoagulationAsync(labRequestDto.Coagulation))?.id : (int?)null,
              OtherLabid = labRequestDto.IsOtherLabChecked ? (await GetOrCreateOtherLabAsync(labRequestDto.OtherLab))?.id : (int?)null
        };

        _context.LaboratoryRequests.Add(laboratoryRequest);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLaboratoryRequest), new { id = laboratoryRequest.Id }, laboratoryRequest);
    }

    private async Task<Parasitology> GetOrCreateParasitologyAsync(ParasitologyDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var parasitology = await _context.Parasitologies
            .Where(p => p.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (parasitology != null)
        {
            return parasitology;
        }

        parasitology = new Parasitology
        {
            StoolDirect = dto.StoolDirect,
            Urinalysis = dto.Urinalysis
        };

        _context.Parasitologies.Add(parasitology);
        await _context.SaveChangesAsync();

        return parasitology;
    }

    private async Task<Hematology> GetOrCreateHematologyAsync(HematologyDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var hematology = await _context.Hematologies
            .Where(h => h.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (hematology != null)
        {
            return hematology;
        }

        hematology = new Hematology
        {
            CBC = dto.CBC,
            Hgb = dto.Hgb,
            ESR = dto.ESR,
            Bloodgroup = dto.Bloodgroup,
            Hba1c = dto.Hba1c,
            BloodFilm = dto.BloodFilm,
            Periferialmorphology = dto.Periferialmorphology,
            MalariaByAgCard = dto.MalariaByAgCard
        };


    _context.Hematologies.Add(hematology);
        await _context.SaveChangesAsync();

        return hematology;
    }

    private async Task<Biochemistry> GetOrCreateBiochemistryAsync(BiochemistryDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var biochemistry = await _context.Biochemistries
            .Where(b => b.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (biochemistry != null)
        {
            return biochemistry;
        }

        biochemistry = new Biochemistry
        {
            FBS = dto.FBS,
            RBS = dto.RBS,
            BUN = dto.BUN,
            Creatinine = dto.Creatinine,
            SGOT = dto.SGOT,
            SGPT = dto.SGPT,
            AlkaPho = dto.AlkaPho,
            TBilirubin = dto.TBilirubin,
            DBilirubin = dto.DBilirubin,
            Albumin = dto.Albumin,
            UricAcid = dto.UricAcid,
            TProtein = dto.TProtein,
            TCholesterol = dto.TCholesterol,
            HDL = dto.HDL,
            LDL = dto.LDL,
            Triglyceride = dto.Triglyceride,
            AMYLASE = dto.AMYLASE,
            GGT = dto.GGT,
            Lipase = dto.Lipase,
            LDH= dto.LDH,
        };

        _context.Biochemistries.Add(biochemistry);
        await _context.SaveChangesAsync();

        return biochemistry;
    }

    private async Task<Bacteriology> GetOrCreateBacteriologyAsync(BacteriologyDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var bacteriology = await _context.Bacteriologies
            .Where(b => b.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (bacteriology != null)
        {
            return bacteriology;
        }

        bacteriology = new Bacteriology
        {
            AFB = dto.AFB,
            WetSmear = dto.WetSmear,
            GramStrin = dto.GramStrin,
            KOH=dto.KOH,

};

        _context.Bacteriologies.Add(bacteriology);
        await _context.SaveChangesAsync();

        return bacteriology;
    }

    private async Task<Serology> GetOrCreateSerologyAsync(SerologyDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var serology = await _context.Serologies
            .Where(s => s.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (serology != null)
        {
            return serology;
        }

        serology = new Serology
        {
            HIV = dto.HIV,
            VDRL = dto.VDRL,
            WeilFelix = dto.WeilFelix,
            BloodGroup = dto.BloodGroup,
            XMatch = dto.XMatch,
            HBsAb=dto.HBsAb,
            HBsAg=dto.HBsAg,
            HPyloriAb=dto.HPyloriAb,
            HPyloriAgStool=dto.HPyloriAgStool,
            RF=dto.RF,
            ASO=dto.ASO,
            CRP=dto.CRP,
            HCG=dto.HCG,
            FOB=dto.FOB,
            HepatitisBViralLoad=dto.HepatitisBViralLoad,
            HepatitisCViralLoad=dto.HepatitisCViralLoad

        };

    _context.Serologies.Add(serology);
        await _context.SaveChangesAsync();

        return serology;
    }

    private async Task<CardiacMarker> GetOrCreateCardiacMarkerAsync(CardiacMarkerDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var cardiacMarker = await _context.CardiacMarkers
            .Where(c => c.id == dto.id)
            .FirstOrDefaultAsync();

        if (cardiacMarker != null)
        {
            return cardiacMarker;
        }

        cardiacMarker = new CardiacMarker
        {
            CKMB = dto.CKMB,
            TroponinT = dto.TroponinT,
            DDimer = dto.DDimer
        };
   

    _context.CardiacMarkers.Add(cardiacMarker);
        await _context.SaveChangesAsync();

        return cardiacMarker;
    }

    private async Task<CancerMarker> GetOrCreateCancerMarkerAsync(CancerMarkerDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var cancerMarker = await _context.CancerMarkers
            .Where(c => c.id == dto.id)
            .FirstOrDefaultAsync();

        if (cancerMarker != null)
        {
            return cancerMarker;
        }

        cancerMarker = new CancerMarker
        {
            CA199 = dto.CA199,
            CA125 = dto.CA125,
            CEA = dto.CEA,
            CA153= dto.CA153,
            AFP= dto.AFP,
        };
       
    

    _context.CancerMarkers.Add(cancerMarker);
        await _context.SaveChangesAsync();

        return cancerMarker;
    }

    private async Task<Electrolyte> GetOrCreateElectrolyteAsync(ElectrolyteDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var electrolyte = await _context.Electrolytes
            .Where(e => e.id == dto.id)
            .FirstOrDefaultAsync();

        if (electrolyte != null)
        {
            return electrolyte;
        }

        electrolyte = new Electrolyte
        {
            Sodium = dto.Sodium,
            Potassium = dto.Potassium,
            Chloride = dto.Chloride,
            Calcium = dto.Calcium,
            Magnesium=dto.Magnesium,
            Phosphorus=dto.Phosphorus
      
};

        _context.Electrolytes.Add(electrolyte);
        await _context.SaveChangesAsync();

        return electrolyte;
    }

    private async Task<Hormone> GetOrCreateHormoneAsync(HormoneDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var hormone = await _context.Hormones
            .Where(h => h.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (hormone != null)
        {
            return hormone;
        }

        hormone = new Hormone
        {
            TSH = dto.TSH,
            FreeT4 = dto.FreeT4,
            FreeT3 = dto.FreeT3,
            TotalT4 = dto.TotalT4,
            TotalT3= dto.TotalT3,
            TotalBetaHCGT3 = dto.TotalBetaHCGT3,
            PSA =dto. PSA,
            FSH=dto.FSH,
            Prolactin=dto.Prolactin,
            LH =dto.LH,

        };
        


        _context.Hormones.Add(hormone);
        await _context.SaveChangesAsync();

        return hormone;
    }

    private async Task<Coagulation> GetOrCreateCoagulationAsync(CoagulationDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var coagulation = await _context.Coagulations
            .Where(c => c.id == dto.id)
            .FirstOrDefaultAsync();

        if (coagulation != null)
        {
            return coagulation;
        }

        coagulation = new Coagulation
        {
            PT = dto.PT,
            APTT = dto.APTT,
            INR = dto.INR,

        };

        _context.Coagulations.Add(coagulation);
        await _context.SaveChangesAsync();

        return coagulation;
    }
    private async Task<OtherLab> GetOrCreateOtherLabAsync(OtherLabDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var otherLab = await _context.OtherLabs
            .Where(o => o.id == dto.id)
            .FirstOrDefaultAsync();

        if (otherLab != null)
        {
            return otherLab;
        }

        otherLab = new OtherLab
        {
            VitB12e = dto.VitB12e,
            VitD = dto.VitD
        };

        _context.OtherLabs.Add(otherLab);
        await _context.SaveChangesAsync();

        return otherLab;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutLaboratoryRequest(int id, [FromBody] LaboratoryRequestDto labRequestDto)
    {
        // Check if the ID matches
        if (id != labRequestDto.Id)
        {
            return BadRequest();
        }

        // Retrieve the existing LaboratoryRequest from the database
        var labRequest = await _context.LaboratoryRequests
            .Include(l => l.Parasitology)
            .Include(l => l.Hematology)
            .Include(l => l.Biochemistry)
            .Include(l => l.Bacteriology)
            .Include(l => l.Serology)
              .Include(l => l.CardiacMarker)
            .Include(l => l.CancerMarker)
            .Include(l => l.Electrolyte)
            .Include(l => l.OtherLab)
            .Include(l => l.Hormone)
            .Include(l => l.Coagulation)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (labRequest == null)
        {
            return NotFound();
        }

        // Update only the checked/unchecked flags
        labRequest.IsBacteriologyChecked = labRequestDto.IsBacteriologyChecked;
        labRequest.IsBiochemistryChecked = labRequestDto.IsBiochemistryChecked;
        labRequest.IsHematologyChecked = labRequestDto.IsHematologyChecked;
        labRequest.IsParasitologyChecked = labRequestDto.IsParasitologyChecked;
        labRequest.IsSerologyChecked = labRequestDto.IsSerologyChecked;
        labRequest.IsOtherLabChecked = labRequestDto.IsOtherLabChecked;
        labRequest.IsCardiacMarkerChecked=labRequestDto.IsCardiacMarkerChecked;
        labRequest.IsCancerMarkerChecked=labRequestDto.IsCancerMarkerChecked;
        labRequest.IsElectrolyteChecked=labRequestDto.IsElectrolyteChecked;
        labRequest.IsHormoneChecked=labRequestDto.IsHormoneChecked;
        labRequest.IsCoagulationChecked=labRequestDto.IsCoagulationChecked;

        // Optionally, update related entities if needed
        if (labRequestDto.IsElectrolyteChecked)
        {
            labRequest.Electrolyte = await GetOrCreateElectrolyteAsync(labRequestDto.Electrolyte);
        }
        else
        {
            labRequest.Electrolyte = null;
        }
        if (labRequestDto.IsCancerMarkerChecked)
        {
            labRequest.CancerMarker = await GetOrCreateCancerMarkerAsync(labRequestDto.CancerMarker);
        }
        else
        {
            labRequest.CancerMarker = null;
        }
        if (labRequestDto.IsCardiacMarkerChecked)
        {
            labRequest.CardiacMarker = await GetOrCreateCardiacMarkerAsync(labRequestDto.CardiacMarker);
        }
        else
        {
            labRequest.CardiacMarker = null;
        }
        if (labRequestDto.IsCoagulationChecked)
        {
            labRequest.Coagulation = await GetOrCreateCoagulationAsync(labRequestDto.Coagulation);
        }
        else
        {
            labRequest.Coagulation = null;
        }
        if (labRequestDto.IsHormoneChecked)
        {
            labRequest.Hormone = await GetOrCreateHormoneAsync(labRequestDto.Hormone);
        }
        else
        {
            labRequest.Hormone = null;
        }
        if (labRequestDto.IsOtherLabChecked)
        {
            labRequest.OtherLab = await GetOrCreateOtherLabAsync(labRequestDto.OtherLab);
        }
        else
        {
            labRequest.OtherLab = null;
        }
        if (labRequestDto.IsParasitologyChecked)
        {
            labRequest.Parasitology = await GetOrCreateParasitologyAsync(labRequestDto.Parasitology);
        }
        else
        {
            labRequest.Parasitology = null;  
        }

        if (labRequestDto.IsHematologyChecked)
        {
            labRequest.Hematology = await GetOrCreateHematologyAsync(labRequestDto.Hematology);
        }
        else
        {
            labRequest.Hematology = null;  // Or handle accordingly if the entity should be removed
        }

        if (labRequestDto.IsBiochemistryChecked)
        {
            labRequest.Biochemistry = await GetOrCreateBiochemistryAsync(labRequestDto.Biochemistry);
        }
        else
        {
            labRequest.Biochemistry = null;  // Or handle accordingly if the entity should be removed
        }

        if (labRequestDto.IsBacteriologyChecked)
        {
            labRequest.Bacteriology = await GetOrCreateBacteriologyAsync(labRequestDto.Bacteriology);
        }
        else
        {
            labRequest.Bacteriology = null;  // Or handle accordingly if the entity should be removed
        }

        if (labRequestDto.IsSerologyChecked)
        {
            labRequest.Serology = await GetOrCreateSerologyAsync(labRequestDto.Serology);
        }
        else
        {
            labRequest.Serology = null;  // Or handle accordingly if the entity should be removed
        }

        // Set the entity state to Modified
        _context.Entry(labRequest).State = EntityState.Modified;

        try
        {
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LaboratoryRequestExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }


    // DELETE: api/LaboratoryRequests/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLaboratoryRequest(int id)
    {
        var labRequest = await _context.LaboratoryRequests.FindAsync(id);
        if (labRequest == null)
        {
            return NotFound();
        }

        _context.LaboratoryRequests.Remove(labRequest);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LaboratoryRequestExists(int id)
    {
        return _context.LaboratoryRequests.Any(e => e.Id == id);
    }
}
