import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LaboratoryRequest } from '../../../Models/labrequest.model';
import { DoctorService } from '../../../app/service/doctor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-labresultform',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './labresultform.component.html',
  styleUrls: ['./labresultform.component.scss']
})
export class LabresultformComponent implements OnInit {
  request?: LaboratoryRequest;
  categories: string[] = [];
  availableTests: string[] = [];
  specialTests = ['bloodFilm', 'peripheralMorphology','bloodGroup','malariaByAgCard','weilFelix','widal','afb','gramStrin','koh','stoolDirect'];
  public selectionOneOptions: string[] = [];
public colorOptionsForTest:string[]=[];
cellCountOptions: string[] = ['No CellCount '];
phaseOptionsForTest: string[] = ['No Phase'];
subTests: any[] = [];
patientId!: number;
currentSubTestIndex: number = 0;


moveToNextSubTest(): void {
  const nextIndex = this.currentSubTestIndex + 1;
  if (nextIndex < this.newTestResult.subTests.length) {
    this.currentSubTestIndex = nextIndex;
    this.newTestResult.subTests[this.currentSubTestIndex].gender = this.newTestResult.gender;
  } else {
    this.currentSubTestIndex = this.newTestResult.subTests.length - 1;
  }
}

// Move to the previous sub-test
moveToPreviousSubTest(): void {
  const previousIndex = this.currentSubTestIndex - 1;
  if (previousIndex >= 0) {
    this.currentSubTestIndex = previousIndex;
    this.newTestResult.subTests[this.currentSubTestIndex].gender = this.newTestResult.gender;
  }
}
consistencyOptions: { [key: string]: string[] } = {
  'urineMicroscopy': ['Clear', 'Turbid', 'Soft', 'Formed', 'Mucoid', 'Watery', 'Diarrhea'],
  'stoolDirect': ['Clear', 'Turbid', 'Soft', 'Formed', 'Mucoid', 'Watery', 'Diarrhea'],
  'afb':['watery', 'Purulent', 'Bloody' ]
};


consistencyOptionsForTest: string[] = ['No Consistency Test'];
phaseOptions: { [test: string]: string[] } = {
  'totalBetaHCGT3': ['Early pregnancy (3-4 weeks from LMP)', 'Early pregnancy (4-5 weeks from LMP)', 'Early pregnancy (5-6 weeks from LMP)', 'Non-pregnant', 'Normal'],
  'FSH': ['Normal', 'Follicular phase', 'Midcycle peak', 'Luteal phase', 'Postmenopausal'],
  'lh': ['Normal', 'Follicular phase', 'Midcycle peak', 'Luteal phase', 'Postmenopausal'],
  'prolactin': ['Normal', 'Non-Pregnant Females', 'Pregnancy','First trimester','Second trimester','Third trimester'],
  'ggt': ['Normal', '2-hour plasma glucose (after 75g glucose load', 'Fasting plasma glucose','Impaired glucose tolerance'],
  'ldh': ['Serum', 'CSF-Adult', 'CSF-NewBorn'],
};

updatePhaseOptions(): void {
  if (this.newTestResult.Test in this.phaseOptions) {
    this.phaseOptionsForTest = this.phaseOptions[this.newTestResult.Test];
  } else {
    this.phaseOptionsForTest = [];
  }
}
isPositive(value: any): boolean {
  return value && value.toLowerCase() === 'positive';
}


updateTestSelections() {

  this.selectionOneOptions = this.testOptions[this.newTestResult.Test] || [];
  
 
  if (this.colorOptions[this.newTestResult.Test]) {
    this.colorOptionsForTest = this.colorOptions[this.newTestResult.Test];
  } else {
    this.colorOptionsForTest = ['No Color Test'];
  }
  

  if (this.consistencyOptions[this.newTestResult.Test]) {
    this.consistencyOptionsForTest = this.consistencyOptions[this.newTestResult.Test];
  } else {
    this.consistencyOptionsForTest = ['No'];
  }
    
    if (this.newTestResult.Test === 'bloodFilm') {
      this.cellCountOptions = [
        'No of cells /Hpf',
        'Many',
        'Moderate',
        'Few'
      ];
    } else if (this.newTestResult.Test === 'peripheralMorphology') {
      this.cellCountOptions = [
        'Few',
        'Moderate',
        'Many'
      ];
    } 
    else if (this.newTestResult.Test === 'stoolDirect') {
      this.cellCountOptions = [
        'No of parasite /Hpf',
        'Many',
        'Moderate',
        'Few'
        
      ];
    }
    else if (this.newTestResult.Test === 'urineMicroscopy') {
      this.cellCountOptions = [
        'No of cell/ HPF',
        'Many',
        'Moderate',
        'Few',
        
        
        
      ];
    }
    else if (this.newTestResult.Test === 'gramStrin') {
      this.cellCountOptions = [
        'No bacteria seen',
        'Many',
        'Moderate',
        'Few'
        
      ];
    }


    else {
      this.cellCountOptions = ['No'];
    }
    this.updateSubTests(); 
}


  // Define your color options for specific tests
colorOptions: { [test: string]: string[] } = {
  'urineMicroscopy': ['Black', 'Red', 'White', 'Dark Yellow/Amber', 'Red or Pink', 'Orange', 'Brown', 'Cloudy or Murky'],
  'stoolDirect': ['Black', 'Red', 'White', 'Dark Yellow/Amber', 'Red or Pink', 'Orange', 'Brown', 'Cloudy or Murky'],
  'afb':['Clear', 'White']
};

  
  newTestResult: any = {
    Category: '',
    Test: '',
    Result: '',
    cellCount: '',
    gender: '',
    PatientId: '',
    Age: '',
    IsPregnant: false,
    Phase: '',
    color: '',
  consistency: '',
  selectionone: '',
  subTests: [],
  ph: '',
  specificGravity: '',
  leukocyteEsterase: '',
  leukocyteEsteraseIntensity: '',
  nitrite: '',
  nitriteIntensity: '',
  urobilinogen: '',
  urobilinogenIntensity: '',
  protein: '',
  proteinIntensity: '',
  glucose: '',
  glucoseIntensity: '',
  bloodHemoglobin: '',
  bloodHemoglobinIntensity:'',
  ketones: '',
  ketonesIntensity: '',
  bilirubin: '',
  bilirubinIntensity: '',
  testDate:'',
  
  };

  private apiUrlBloodFilm = 'http://localhost:5153/api/LabBloodFilmTest';
  private apiUrlOtherTests = 'http://localhost:5153/api/LaboratoryTestResult';

  constructor(private labRequestService: DoctorService, private route: ActivatedRoute, private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const requestId = +params['id'];
      this.loadRequest(requestId);
    });
    // this.onTestChange();
    this.updateTestSelections(); 

    const today = new Date().toISOString().split('T')[0]; 
    this.newTestResult.testDate = today;
  }
  
  loadRequest(id: number): void {
    this.labRequestService.getRequestById(id).subscribe(
      data => {
        this.request = data;
        this.initializeCategories();
        this.patientId = this.request!.patientId; 
        this.newTestResult.PatientId = this.patientId;
      },
      error => {
        console.error('Error fetching request data', error);
      }
    );
  }

  initializeCategories(): void {
    if (this.request) {
      this.categories = [];
      if (this.request.isBiochemistryChecked) this.categories.push('Biochemistry');
      if (this.request.isBacteriologyChecked) this.categories.push('Bacteriology');
      if (this.request.isSerologyChecked) this.categories.push('Serology');
      if (this.request.isElectrolyteChecked) this.categories.push('Electrolyte');
      if (this.request.isCancerMarkerChecked) this.categories.push('Cancer Marker');
      if (this.request.isCardiacMarkerChecked) this.categories.push('Cardiac Marker');
      if (this.request.isCoagulationChecked) this.categories.push('Coagulation');
      if (this.request.isHormoneChecked) this.categories.push('Hormone');
      if (this.request.isOtherLabChecked) this.categories.push('Other Lab');
      if (this.request.isParasitologyChecked) this.categories.push('Parasitology');
      if (this.request.isHematologyChecked) this.categories.push('Hematology');
    }
  }

  onCategoryChange(): void {
    if (this.request && this.newTestResult.Category) {
      this.availableTests = this.getTestsForCategory(this.newTestResult.Category);
    }
  }


  getTestsForCategory(category: string): string[] {
    if (!this.request) return [];
    switch (category) {
      case 'Biochemistry':
        return this.getAvailableTests(this.request.biochemistry);
      case 'Bacteriology':
        return this.getAvailableTests(this.request.bacteriology);
      case 'Serology':
        return this.getAvailableTests(this.request.serology);
      case 'Electrolyte':
        return this.getAvailableTests(this.request.electrolyte);
      case 'Cancer Marker':
        return this.getAvailableTests(this.request.cancerMarker);
      case 'Cardiac Marker':
        return this.getAvailableTests(this.request.cardiacMarker);
      case 'Coagulation':
        return this.getAvailableTests(this.request.coagulation);
      case 'Hormone':
        return this.getAvailableTests(this.request.hormone);
      case 'Other Lab':
        return this.getAvailableTests(this.request.otherLab);
      case 'Parasitology':
        return this.getAvailableTests(this.request.parasitology);
      case 'Hematology':
        return this.getAvailableTests(this.request.hematology);
      default:
        return [];
    }
  }

  getAvailableTests(items: any): string[] {
    if (!items) return [];
    return Object.keys(items).filter(key => items[key]);
  }

  isSpecialTest(): boolean {
    return this.specialTests.includes(this.newTestResult.Test);
  }

  saveTestResult(): void {
    if (!this.newTestResult.Category || !this.newTestResult.Test) {
      alert('Please fill in all required fields.');
      return;
    }
   
    let apiUrl = '';
    let dataToSend: any = {};
  
    if (this.newTestResult.Test === 'urinalysis') {
      // For Urine Dipstick Test
      apiUrl = 'http://localhost:5153/api/UrineDipsticksTest';
      dataToSend = {
        category: this.newTestResult.Category,
        test: this.newTestResult.Test,
        ph: this.newTestResult.ph,
        specificGravity: this.newTestResult.specificGravity,
        leukocyteEsterase: this.newTestResult.leukocyteEsterase,
        leukocyteEsteraseIntensity: this.newTestResult.leukocyteEsteraseIntensity,
        nitrite: this.newTestResult.nitrite,
        nitriteIntensity: this.newTestResult.nitriteIntensity,
        urobilinogen: this.newTestResult.urobilinogen,
        urobilinogenIntensity: this.newTestResult.urobilinogenIntensity,
        protein: this.newTestResult.protein,
        proteinIntensity: this.newTestResult.proteinIntensity,
        glucose: this.newTestResult.glucose,
        glucoseIntensity: this.newTestResult.glucoseIntensity,
        bloodHemoglobin: this.newTestResult.bloodHemoglobin,
        bloodHemoglobinIntensity:this.newTestResult.bloodHemoglobinIntensity,
        ketones: this.newTestResult.ketones,
        ketonesIntensity: this.newTestResult.ketonesIntensity,
        bilirubin: this.newTestResult.bilirubin,
        bilirubinIntensity: this.newTestResult.bilirubinIntensity,
        testDate:this.newTestResult.testDate,
        PatientId: this.newTestResult.PatientId,
      };
    } else if (this.isSpecialTest()) {
      // For special tests
      apiUrl = this.apiUrlBloodFilm;
      dataToSend = {
        category: this.newTestResult.Category,
        test: this.newTestResult.Test,
        cellCount: this.newTestResult.cellCount,
        consistency: this.newTestResult.consistency,
        selectionone: this.newTestResult.selectionone,
        color: this.newTestResult.color,
        PatientId: this.newTestResult.PatientId,
      };
    } else {
      // For other tests
      apiUrl = this.apiUrlOtherTests;
  
      if (this.newTestResult.Test !== 'cbc') {
        this.newTestResult.subTests = [];
      }
      dataToSend = {
        Category: this.newTestResult.Category,
        Test: this.newTestResult.Test,
        Result: this.newTestResult.Result,
        PatientId: this.newTestResult.PatientId,
        Age: this.newTestResult.Age,
        IsPregnant: this.newTestResult.IsPregnant,
        Phase: this.newTestResult.Phase,
        Gender: this.newTestResult.Gender,
        subTests: this.newTestResult.subTests
      };
    }
  
    this.http.post(apiUrl, dataToSend).subscribe(
      response => {
        alert('Test result registered successfully.');
        this.router.navigate(['/lablist']);
      },
      error => {
        alert('An error occurred while saving the test result.');
        console.error('Error:', error);
      }
    );
  }
  


  private testOptions: { [testName: string]: string[] } = {
    'bloodFilm': [
      'No Haemoparasite seen',
      'Trophozoit of Plasmodium falciparum seen',
      'Trophozoit of P. Vivax',
      'Ring form of Plasmodium falciparum seen',
      'Ring form of P. Vivax',
      'Gametocyte of Plasmodium falciparum seen',
      'Gametocyte of P. Vivax',
      'Mixed infection of Plasmodium falciparum and P. Vivax'
    ],
    'peripheralMorphology': [
      'Normal Morphology',
      'Red Blood Cells (RBCs)',
      'White Blood Cells (WBCs)',
      'Platelets',
      'Anisocytosis',
      'Poikilocytosis',
      'Target Cells',
      'Sickle Cells',
      'Schistocytes',
      'Hypochromia',
      'Microcytes',
      'Macrocytes',
      'Heinz Bodies',
      'Neutrophils',
      'Lymphocytes',
      'Monocytes',
      'Eosinophils',
      'Blasts',
     
    ],
    'stoolDirect':[
  'No Ova of parasite seen',
	'Trohozoit of Giardia lamblia Seen',
	'Cyst  of Giardia lamblia Seen',
	'Trohozoit of Entamoeba histolytica Seen',
	'Cyst  of  Entamoeba histolytica Seen',
	'Eggs of Ascaris lumbricoides Seen',
	'Eggs of Trichuris trichiura  Seen',
	'Eggs of  Hookworm',
	'Larva of Strongyloides stercoralis  Seen',
	'Eggs of Taenia species',
	'Puss cell seen',
	'Rbc seen',

    ],
    'afb':['1+: 1-9 AFB per 100 fields',
      '2+: 1-9 AFB per 10 fields',
      '3+: 1-9 AFB per field',
      '4+: >9 AFB per field',
      'Negative AFB Smear'
      ],
    'bloodGroup':[
      'O+VE', 'A+VE', 'B+VE', 'AB+VE', 'O-VE', 'A-VE', 'B-VE', 'AB-VE',
    ],
    'malariaByAgCard':['Positive','Negative'],
    'hbsAg': ['Negative', 'Positive'],
    'hcv': ['Negative', 'Positive'],
    'syphilis': ['Negative', 'Positive'],
    'hiv': ['Negative', 'Positive'],
    'hPyloriAgStool': ['Negative', 'Positive'],
    'hPyloriAbSerum': ['Negative', 'Positive'],
    'rf': ['Reactive', 'Non-Reactive'],
    'aso': ['Reactive', 'Non-Reactive'],
    'fob':['Negative', 'Positive'],
    'hcg':['Negative', 'Positive'],
    'weilFelix':['1:40','1:80:160', '1:320'],
    'widal':['1:40','1:80','1:160','1:320'],
    'gramStrin':['No bacteria seen',
      'Gram-Positive Bacteria',
      'Gram-Negative Bacteria',
      'Cocci',
      'Bacilli',
      'Spirilla',
      'Fungi'
      ],
      'koh':['No fungal  Elements seen',
        'Fungal Elements seen',
        'Yeast Cells seen',
        'Pseudohyphae',
        'Hyphae seen'
        ]
  };

  updateSubTests(): void {
    if (this.newTestResult.Test === 'cbc') {
      this.newTestResult.subTests = [
        { name: 'WBC', result: '', gender:'' },
        { name: 'Neu', result: '', gender: '' },
        { name: 'Lym', result: '', gender: '' },
        { name: 'Mon', result: '', gender: '' },
        { name: 'Eos', result: '', gender: '' },
        { name: 'Bas', result: '', gender: ''},
        { name: 'Neu', result: '', gender: '' },
        { name: 'Lym', result: '', gender: '' },
        { name: 'Mon', result: '', gender: ''},
        { name: 'Eos', result: '', gender:'' },
        { name: 'Bas', result: '', gender: '' },
        { name: 'RBC', result: '', gender: '' },
        { name: 'HGB', result: '', gender: '' },
        { name: 'HCT', result: '', gender: ''},
        { name: 'MCV', result: '', gender: '' },
        { name: 'MCH', result: '', gender: '' },
        { name: 'MCHC', result: '', gender: '' },
        { name: 'RDW-CV', result: '', gender: '' },
        { name: 'RDW-SD', result: '', gender:'' },
        { name: 'PLT', result: '', gender: '' },
        { name: 'MPV', result: '', gender: ''},
        { name: 'PDW', result: '', gender: '' },
        { name: 'PCT', result: '', gender: '' },
        { name: 'p-LCC', result: '', gender: '' },
        { name: 'P-LCR', result: '', gender: '' }
      ];
    } else {
      this.newTestResult.subTests = [];
    }
  }
}
