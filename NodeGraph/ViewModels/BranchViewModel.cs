using NodeGraph.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.ViewModels
{
    public class BranchViewModel : InputBase, IDisposable
    {
        private BranchDialogueViewModel _currentDialogue;

        public BranchDialogueViewModel CurrentDialogue
        {
            get => _currentDialogue;
            set => SetProperty(ref _currentDialogue, value);
        }
        public ObservableCollection<BranchDialogueViewModel> Dialogues { get; private set; }

        public BranchViewModel()
        {
            Dialogues = new ObservableCollection<BranchDialogueViewModel>();
            Dialogues.CollectionChanged += Dialogues_CollectionChanged;
            Dialogues.Add(new BranchDialogueViewModel());
            Dialogues.Add(new BranchDialogueViewModel());
            Dialogues.Add(new BranchDialogueViewModel());
        }

        private void Dialogues_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                BranchDialogueViewModel dialogue = Dialogues[e.NewStartingIndex];
                dialogue.OutputClicked += Dialogue_OutputClicked;
            }
        }

        private void Dialogue_OutputClicked(object sender, EventArgs e)
        {
            Console.WriteLine("branch output clicked");
        }

        public void Dispose()
        {
            foreach (BranchDialogueViewModel dialogue in Dialogues) {
                dialogue.OutputClicked -= Dialogue_OutputClicked;
            }
        }
    }
}
